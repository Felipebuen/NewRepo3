using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contas.LibClasses
{
    public class Carteira
    {
        public double Saldo
        {
            get;
            private set;
        }
        private static double IDAnterior = 0;

        public Carteira()
        {
            IDAnterior++;
            this.ID = IDAnterior;
        }
        public string Dono { get; set; }

        public double ID { get; set; }
        
        public string CPF { get; set; } 

        public bool TarifaCobrada { get; set; }

        public double LimiteConta { get; set; }

        public DateTime DataUltimaCobranca { get; set; }

        public bool Sacar(double Valor, DateTime hora)
        {
            if (!(hora.Hour > 8 && hora.Hour < 18))
            {
                return false;
            }
            return this.Sacar(Valor);
        }
        public bool Sacar(double Valor)
        {
            if (Valor > this.Saldo)
                return false;

            this.Saldo -= Valor;
            //this.Saldo = Saldo - Valor;
            return true;
        }

        public void CobrarTarifas()
        {
            if (DataUltimaCobranca.Month != DateTime.Today.Month)
            {
                this.Saldo -= 19.90;
                DataUltimaCobranca = DateTime.Today;
                TarifaCobrada = true;
                Console.WriteLine($"Cobrança realizada para a conta {Dono} no valor de R$19,90.");
                
            }
            else
            {
                Console.WriteLine($"A tarifa já foi cobrada este mês para a conta {Dono}.");
            }
        }

        public bool Depositar(double Valor)
        {
            this.Saldo += Valor;
            return true;
        }
        public bool Depositar(double Valor, DateTime hora)
        {
            if (!(hora.Hour > 8 && hora.Hour < 18))
            {
                return false;
            }
            return this.Sacar(Valor);
        }
        public bool Transferir
            (Carteira destino, double valor)
        {  
            //se nao tiver saldo cancela transferencia retornando false
            if (this.Saldo <= valor)
                return false;

            //Executa transferencia tirando da conta origram e deposinto na conta destino
            this.Sacar(valor);
            bool tOK = destino.Depositar(valor);
            if (tOK)// se transferencia ocorreu com sucesso retorna true
                return true;
            else// caso ocorrer erro faz o rollback voltando dinheiro para conta de origem
            {
                this.Depositar(valor);
                return false;
            }
        }
    }
}
