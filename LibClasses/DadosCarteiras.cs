namespace Contas.LibClasses
{
    public class DadosCarteiras
    {
        public List<Carteira> ListaDados = new List<Carteira>();
        public DateTime DataDoSistema { get; set; } = DateTime.Now;

        public void CobrarCarteiras()
        {
            foreach (var conta in ListaDados)
            {
                conta.CobrarTarifas();

            }
        }

      
    }
}
