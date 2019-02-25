using System;
using System.Collections.Generic;
using aula02DevOpsPoker.Poco;

namespace aula02DevOpsPoker.Objetos
{
    public class Naipes{
        public List<Naipe> NaipesRetorno = new List<Naipe>();

        public List<Naipe> GerarNipes(){
            Naipe naipe = new Naipe();
            string[] letras = {"D", "H", "S", "C"};
            int count = 0;

            foreach (var item in letras)
            {                
                naipe.Id = count;
                naipe.NipeNome = item;
                naipe.Peso = count;
                NaipesRetorno.Add(naipe);
                naipe = new Naipe();    
                count = count + 1;            
            }

            return NaipesRetorno;
        }
    }
}