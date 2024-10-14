using System;
using System.Collections.Generic;

namespace Application_web_Arquitecture.Models;

public partial class Transaccione
{
    public int IdTransaccion { get; set; }

    public int IdUsuario { get; set; }

    public int IdVideojuego { get; set; }

    public string TipoTransaccion { get; set; } = null!;

    public decimal Precio { get; set; }

    public DateTime Fecha { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual Videojuego IdVideojuegoNavigation { get; set; } = null!;
}
