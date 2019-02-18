using System;
using System.Collections.Generic;

namespace aula02DevOpsPoker.Objetos
{
    public class Cartas{
        public List<object> CartasRetorno = new List<object>();

        public List<object> GerarCartas(){
            for (int i = 1; i < 9; i++)
            {
                var valor = i + 1; 
                CartasRetorno.Add(valor);
            }

            CartasRetorno.Add("J");
            CartasRetorno.Add("Q");
            CartasRetorno.Add("K");
            CartasRetorno.Add("A");

            return CartasRetorno;
        }
    }
}