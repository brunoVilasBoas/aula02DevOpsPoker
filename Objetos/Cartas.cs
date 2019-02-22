using System;
using System.Collections.Generic;
using aula02DevOpsPoker.Poco;

namespace aula02DevOpsPoker.Objetos
{
    public class Cartas{
        public List<Carta> CartasRetorno = new List<Carta>();

        public List<Carta> GerarCartas(){
            Carta carta = new Carta();
            string[] letras = {"J", "Q", "K", "A"};

            for (int i = 1; i <= 9; i++)
            {
                carta.Id = i;
                carta.CartaNome = (i + 1).ToString();
                carta.Peso = i;
                CartasRetorno.Add(carta);
                carta = new Carta();
            }

            int count = 0;
            int peso = 9;
            foreach (var item in letras)
            {
                count = count++;
                carta.Id = count;
                carta.CartaNome = item;
                carta.Peso = peso++;
                CartasRetorno.Add(carta);
                carta = new Carta();
            }

            return CartasRetorno;
        }
    }
}