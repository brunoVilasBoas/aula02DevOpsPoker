using System;
using System.Collections.Generic;
using aula02DevOpsPoker.Poco;

namespace aula02DevOpsPoker.Objetos
{
    public class Nipes{
        public List<Nipe> NipesRetorno = new List<Nipe>();

        public List<Nipe> GerarNipes(){
            Nipe nipe = new Nipe();
            string[] letras = {"D", "H", "S", "C"};
            int count = 0;

            foreach (var item in letras)
            {                
                nipe.Id = count;
                nipe.NipeNome = item;
                nipe.Peso = count;
                NipesRetorno.Add(nipe);
                nipe = new Nipe();    
                count = count + 1;            
            }

            return NipesRetorno;
        }
    }
}