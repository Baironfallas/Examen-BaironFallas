namespace NLayer.Architecture.Bussines.Models;

public class Pokemon
{
    public string Nombre { get; set; }
    public string Tipo { get; set; }
    public string UrlSprite { get; set; }

    //lista que contendra los movimientos del pokemon
    public List<string> Moves { get; set; }
}
