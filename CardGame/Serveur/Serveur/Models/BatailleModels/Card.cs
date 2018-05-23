namespace Serveur.Models.BatailleModels
{
    public class Card
    {
        public int Value;
        public string Colour;

        public Card(string colour, int value)
        {
            this.Colour = colour;
            this.Value = value;
        }
    }
}