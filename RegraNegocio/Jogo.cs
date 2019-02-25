using System;
using System.Collections.Generic;
using System.Linq;
using aula02DevOpsPoker.Objetos;
using aula02DevOpsPoker.Poco;

namespace aula02DevOpsPoker.RegraNegocio
{
    public class Jogo
    {
        private Cartas _cartas;
        private Naipes _naipes;
        private List<CartaNaipe> _cartasJogador1 = new List<CartaNaipe>();
        private List<CartaNaipe> _cartasJogador2 = new List<CartaNaipe>();

        public Jogo()
        {
            _cartas = new Cartas();
            _naipes = new Naipes();
        }

        public void Iniciar()
        {
            MontarCartas();
            MostrarCartas();
            MostrarVencedor();
        }

        private void MontarCartas()
        {
            var _cartasGeradas = _cartas.GerarCartas();
            var _naipesGerados = _naipes.GerarNipes();

            for (int i = 0; i < 5; i++)
            {
                _cartasJogador1.Add(GerarCarta(_cartasGeradas, _naipesGerados, _cartasJogador1));
                _cartasJogador2.Add(GerarCarta(_cartasGeradas, _naipesGerados, _cartasJogador2));
            }
        }

        private void MostrarCartas()
        {
            Console.WriteLine("Cartas Jogador 1");
            _cartasJogador1.ForEach(c =>
            {
                Console.WriteLine(string.Format("{0}{1}", c.Carta.CartaNome, c.Naipe.NipeNome));
            });

            Console.WriteLine("Cartas Jogador 2");
            _cartasJogador2.ForEach(c =>
            {
                Console.WriteLine(string.Format("{0}{1}", c.Carta.CartaNome, c.Naipe.NipeNome));
            });
        }

        private T SelecionarItemAleatorio<T>(List<T> lista)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            var selecionado = lista[rand.Next(lista.Count)];

            return selecionado;
        }

        private CartaNaipe GerarCarta(List<Carta> cartas, List<Naipe> naipes, List<CartaNaipe> listaReferancia)
        {
            CartaNaipe cartaNipe = new CartaNaipe
            {
                Carta = SelecionarItemAleatorio<Carta>(cartas),
                Naipe = SelecionarItemAleatorio<Naipe>(naipes)
            };

            while (listaReferancia.Any(a => a.Carta == cartaNipe.Carta && a.Naipe == cartaNipe.Naipe))
            {
                cartaNipe = new CartaNaipe
                {
                    Carta = SelecionarItemAleatorio<Carta>(cartas),
                    Naipe = SelecionarItemAleatorio<Naipe>(naipes)
                };
            }

            return cartaNipe;
        }

        private void MostrarVencedor()
        {
            var vencedor = ValidaCartaAlta(_cartasJogador1, _cartasJogador2);
            var resultado = ValidaParTrinca();
        }

        private EnumVencedor ValidaCartaAlta(List<CartaNaipe> jogador1, List<CartaNaipe> jogador2)
        {
            int cartasMarioresJogador1 = 0;
            int cartasMarioresJogador2 = 0;

            jogador1.ForEach(j1 =>
            {
                jogador2.ForEach(j2 =>
                {
                    if (j1.Carta.Peso > j2.Carta.Peso)
                        cartasMarioresJogador1 = cartasMarioresJogador1 + 1;
                    else if (j2.Carta.Peso > j1.Carta.Peso)
                        cartasMarioresJogador2 = cartasMarioresJogador2 + 1;
                });
            });

            if (cartasMarioresJogador1 > cartasMarioresJogador2)
                return EnumVencedor.Jogador1;
            else if (cartasMarioresJogador1 < cartasMarioresJogador2)
                return EnumVencedor.Jogador2;

            return EnumVencedor.Empate;
        }

        private EnumVencedor ValidaParTrinca()
        {
            var paresJogador1 = _cartasJogador1.GroupBy(g => g)
                                .Where(w => w.Count() > 1)
                                .Select(s => new CartaNaipe { Carta = s.Key.Carta, Naipe = s.Key.Naipe })
                                .ToList();

            var paresJogador2 = _cartasJogador2.GroupBy(g => g)
                                .Where(w => w.Count() > 1)
                                .Select(s => new CartaNaipe { Carta = s.Key.Carta, Naipe = s.Key.Naipe })
                                .ToList();

            if (paresJogador1.Count() == 0 && paresJogador2.Count() == 0)
                return EnumVencedor.Empate;

            return ValidaCartaAlta(paresJogador1, paresJogador2);
        }

        private EnumVencedor ValidaStraight()
        {
            bool straightJogador1 = false;
            bool straightJogador2 = false;

            _cartasJogador1.ForEach(j1 =>
            {
                var id_carta = 0;
                if (j1.Carta.Id > id_carta)
                    straightJogador1 = true;
                else
                    return;
            });

            _cartasJogador2.ForEach(j1 =>
            {
                var id_carta = 0;
                if (j1.Carta.Id > id_carta)
                {
                    straightJogador2 = true;
                    id_carta = j1.Carta.Id;
                }
                else
                    return;
            });

            if (straightJogador1 && straightJogador2)
                return ValidaCartaAlta(_cartasJogador1, _cartasJogador2);

            return EnumVencedor.Empate;
        }

        private EnumVencedor ValidaFlush()
        {
            bool flushJogador1 = false;
            bool flushJogador2 = false;

            flushJogador1 = _cartasJogador1.GroupBy(g => g)
                            .Any(j1 => j1.Count() == 5);

            flushJogador2 = _cartasJogador2.GroupBy(g => g)
                            .Any(j1 => j1.Count() == 5);

            if (flushJogador1 && flushJogador2)
                return ValidaCartaAlta(_cartasJogador1, _cartasJogador2);

            return EnumVencedor.Empate;
        }

        private EnumVencedor ValidaFullHouse()
        {
            var cartasJogador1 = _cartasJogador1.GroupBy(g => g)
                                .Where(w => w.Count() == 3 && w.Count() == 2)
                                .Select(s => new CartaNaipe { Carta = s.Key.Carta, Naipe = s.Key.Naipe })
                                .ToList();

            var cartasJogador2 = _cartasJogador2.GroupBy(g => g)
                                .Where(w => w.Count() == 3 && w.Count() == 2)
                                .Select(s => new CartaNaipe { Carta = s.Key.Carta, Naipe = s.Key.Naipe })
                                .ToList();

            if (cartasJogador1 != null && cartasJogador2 != null)
            {
                cartasJogador1 = cartasJogador1.GroupBy(g => g)
                                 .Select(s => new CartaNaipe { Carta = s.Key.Carta, Naipe = s.Key.Naipe })
                                 .ToList();
                cartasJogador2 = cartasJogador2.GroupBy(g => g)
                                 .Select(s => new CartaNaipe { Carta = s.Key.Carta, Naipe = s.Key.Naipe })
                                 .ToList();

                return ValidaCartaAlta(cartasJogador1, cartasJogador2);
            }

            return EnumVencedor.Empate;
        }

        private EnumVencedor ValidaQuadra()
        {
            var cartasJogador1 = _cartasJogador1.GroupBy(g => g)
                                 .Where(w => w.Count() == 4)
                                 .Select(s => new CartaNaipe { Carta = s.Key.Carta, Naipe = s.Key.Naipe })
                                 .ToList();
            var cartasJogador2 = _cartasJogador2.GroupBy(g => g)
                                 .Where(w => w.Count() == 4)
                                 .Select(s => new CartaNaipe { Carta = s.Key.Carta, Naipe = s.Key.Naipe })
                                 .ToList();

            if(cartasJogador1 != null && cartasJogador2 != null)
                return ValidaCartaAlta(cartasJogador1, cartasJogador2);

            return EnumVencedor.Empate;
        }
    }
}