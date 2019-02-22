using System;
using System.Collections.Generic;
using aula02DevOpsPoker.Objetos;
using aula02DevOpsPoker.Poco;

namespace aula02DevOpsPoker.RegraNegocio
{
    public class Jogo
    {
        private Cartas _cartas;
        private Nipes _nipes;
        private List<CartaNipe> _cartasJogador1 = new List<CartaNipe>();
        private List<CartaNipe> _cartasJogador2 = new List<CartaNipe>();

        public Jogo()
        {
            _cartas = new Cartas();
            _nipes = new Nipes();
        }

        public void Iniciar()
        {
            MontarCartas();
            MostrarCartas();
        }

        private void MontarCartas()
        {
            var _cartasGeradas = _cartas.GerarCartas();
            var _nipesGerados = _nipes.GerarNipes();

            for (int i = 0; i < 5; i++)
            {
                _cartasJogador1.Add(GerarCarta(_cartasGeradas, _nipesGerados, _cartasJogador1));
                _cartasJogador2.Add(GerarCarta(_cartasGeradas, _nipesGerados, _cartasJogador2));
            }
        }

        private void MostrarCartas()
        {
            Console.WriteLine("Cartas Jogador 1");
            _cartasJogador1.ForEach(c => {
                Console.WriteLine(string.Format("{0}{1}", c.Carta.CartaNome, c.Nipe.NipeNome));
            });    

            Console.WriteLine("Cartas Jogador 2");
            _cartasJogador2.ForEach(c => {
                Console.WriteLine(string.Format("{0}{1}", c.Carta.CartaNome, c.Nipe.NipeNome));
            }); 
        }

        private T SelecionarItemAleatorio<T>(List<T> lista)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            var selecionado = lista[rand.Next(lista.Count)];

            return selecionado;
        }

        private CartaNipe GerarCarta(List<Carta> cartas, List<Nipe> nipes, List<CartaNipe> listaReferancia)
        {
            CartaNipe cartaNipe = new CartaNipe
            {
                Carta = SelecionarItemAleatorio<Carta>(cartas),
                Nipe = SelecionarItemAleatorio<Nipe>(nipes)
            };

            //Verificar validação
            while (listaReferancia.Contains(cartaNipe))
            {
                cartaNipe = new CartaNipe
                {
                    Carta = SelecionarItemAleatorio<Carta>(cartas),
                    Nipe = SelecionarItemAleatorio<Nipe>(nipes)
                };
            }

            return cartaNipe;
        }

        //Criar metodo para validar jogador vencedor
    }
}