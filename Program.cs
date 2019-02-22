using System;
using aula02DevOpsPoker.RegraNegocio;

namespace aula02DevOpsPoker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Jogo jogo = new Jogo();
            jogo.Iniciar();
        }
    }
}
