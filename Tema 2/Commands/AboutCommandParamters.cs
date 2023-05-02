namespace Tema_2_MVP.Commands
{
    public class AboutCommandParamters
    {
        public string Nume { get; set; }
        public string Grup { get; set; }

        public AboutCommandParamters(string nume, string group)
        {
            Nume = nume;
            Grup = group;
        }
    }
}
