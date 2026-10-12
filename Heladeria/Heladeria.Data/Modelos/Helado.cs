namespace Heladeria.Data.Modelos;

public class Helado
{
    public int Id { get; set; }
    public string Sabor { get; set; } = string.Empty;
    public string Tamanio { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public bool Borrado { get; set; } = false;
}
