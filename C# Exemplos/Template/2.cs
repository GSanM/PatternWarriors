using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Rextester
{
    abstract class Documento
    {        
        protected Random random = new Random();
        
        public void AdicionaDocumentoAoRepositorio()
        {
            if( VerificarPermissaoDoUsuario() == 1)
            {
                if (CarimbarDocumento() == 1)
                {
                    if (VerificarVeracidade() == 1)
                    {
                        Console.WriteLine("Documento Adicionado ao Repositorio com Sucesso ...");
                    }
                    else
                    {
                        Console.WriteLine("Documento Não Veridico ...");
                    }
                }
                else
                {
                    Console.WriteLine("Erro ao Carimbar Documento ...");
                }
            }
            else
            {
                Console.WriteLine("Usuario Não Permitido ...");
            }
     
        }
        
        public virtual int VerificarPermissaoDoUsuario()
        {
            Console.WriteLine("Verificando Permissão do Usuario ...");
            
            int randomNumber = random.Next(2);
            return randomNumber;
        }
        public virtual int CarimbarDocumento()
        {
            Console.WriteLine("Carimbando Documentos ...");
            int randomNumber = random.Next(2);
            return randomNumber;
        }
        public abstract int VerificarVeracidade();
            
    }
    class Recibo: Documento
    {        
        public override int VerificarVeracidade()
        {            
            Console.WriteLine("Consultando o Sistema de Notas Eletrônicas ...");
            int randomNumber = random.Next(2);
            return randomNumber;
        }
    }
    class Foto: Documento
    {        
        public override int VerificarVeracidade()
        {
            Console.WriteLine("Comparando com Fotos Originais ...");
            int randomNumber = random.Next(2);
            return randomNumber;
        }
    }
    class Certidao: Documento
    {        
        public override int VerificarVeracidade()
        {
            Console.WriteLine("Verificando as Assinaturas do Tabelião ...");
            int randomNumber = random.Next(2);
            return randomNumber;
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            Foto oFoto = new Foto();
            oFoto.AdicionaDocumentoAoRepositorio();
        }
    }
}
