using System;
using System.Collections.Generic;

namespace aula02DevOpsPoker.Objetos
{
    public class Nipes{
        public List<object> NipesRetorno = new List<object>();

        public List<object> GerarNipes(){
            NipesRetorno.Add("D");
            NipesRetorno.Add("H");
            NipesRetorno.Add("S");
            NipesRetorno.Add("C");

            return NipesRetorno;
        }
    }
}