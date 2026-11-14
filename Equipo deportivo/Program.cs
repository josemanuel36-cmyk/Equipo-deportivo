
using System;
using System.Collections.Generic;
using System.Linq;

// Diccionarios que representan las tablas.
Dictionary<int, Dictionary<string, string>> Deportes = new();
Dictionary<int, Dictionary<string, string>> Categorias = new();
Dictionary<int, Dictionary<string, string>> Equipos = new();
Dictionary<int, Dictionary<string, string>> Posiciones = new();
Dictionary<int, Dictionary<string, string>> Jugadores = new();
Dictionary<int, Dictionary<string, string>> EquipoJugador = new();
Dictionary<int, Dictionary<string, string>> Entrenadores = new();
Dictionary<int, Dictionary<string, string>> EquipoEntrenador = new();
Dictionary<int, Dictionary<string, string>> Temporadas = new();
Dictionary<int, Dictionary<string, string>> Partidos = new();

string[] tablas =
{
    "Deportes", "Categorias", "Equipos", "Posiciones", "Jugadores",
    "EquipoJugador", "Entrenadores", "EquipoEntrenador", "Temporadas", "Partidos"
};

// Campos de cada tabla.
Dictionary<string, string[]> campos = new()
{
    ["Deportes"] = new[] { "Nombre", "Descripcion", "Estado" },
    ["Categorias"] = new[] { "Nombre", "EdadMinima", "EdadMaxima" },
    ["Equipos"] = new[] { "Nombre", "Ciudad", "FechaFundacion", "IdDeporte", "IdCategoria", "Estado" },
    ["Posiciones"] = new[] { "Nombre", "IdDeporte" },
    ["Jugadores"] = new[] { "Nombre", "Apellido", "FechaNacimiento", "Telefono", "Correo", "Documento", "Nacionalidad", "Estado" },
    ["EquipoJugador"] = new[] { "IdEquipo", "IdJugador", "IdPosicion", "Dorsal", "FechaInicio", "FechaFin" },
    ["Entrenadores"] = new[] { "Nombre", "Apellido", "Telefono", "Correo", "Especialidad", "ExperienciaAnios", "Estado" },
    ["EquipoEntrenador"] = new[] { "IdEquipo", "IdEntrenador", "Cargo", "FechaInicio", "FechaFin" },
    ["Temporadas"] = new[] { "Nombre", "FechaInicio", "FechaFin", "Estado" },
    ["Partidos"] = new[] { "IdTemporada", "IdEquipoLocal", "IdEquipoVisitante", "FechaPartido", "Lugar", "GolesLocal", "GolesVisitante", "Estado" }
};

string[] opcionales =
{
    "Descripcion", "Telefono", "Correo", "Documento", "Nacionalidad",
    "FechaFundacion", "FechaFin", "Lugar", "Dorsal", "GolesLocal", "GolesVisitante"
};

string[] camposFecha =
{
    "FechaFundacion", "FechaNacimiento", "FechaInicio", "FechaFin", "FechaPartido"
};

string[] camposNumericos =
{
    "EdadMinima", "EdadMaxima", "IdDeporte", "IdCategoria", "IdEquipo",
    "IdJugador", "IdPosicion", "IdEntrenador", "IdTemporada",
    "IdEquipoLocal", "IdEquipoVisitante", "Dorsal", "ExperienciaAnios",
    "GolesLocal", "GolesVisitante"
};

// Menu principal.
int opcionPrincipal = -1;

while (opcionPrincipal != 0)
{
    Console.Clear();
    Console.WriteLine("==============================================");
    Console.WriteLine("       REGISTRO DE EQUIPOS DEPORTIVOS");
    Console.WriteLine("==============================================");

    for (int i = 0; i < tablas.Length; i++)
        Console.WriteLine($"{i + 1}. {tablas[i]}");

    Console.WriteLine("0. Salir");
    Console.WriteLine("==============================================");
    Console.Write("Seleccione una tabla: ");

    if (!int.TryParse(Console.ReadLine(), out opcionPrincipal))
    {
        Console.WriteLine("Debe introducir un número.");
        Pausar();
        opcionPrincipal = -1;
        continue;
    }

    if (opcionPrincipal >= 1 && opcionPrincipal <= tablas.Length)
    {
        string nombre = tablas[opcionPrincipal - 1];
        GestionarTabla(ObtenerTabla(nombre), nombre);
    }
    else if (opcionPrincipal == 0)
    {
        Console.WriteLine("Gracias por utilizar el sistema.");
    }
    else
    {
        Console.WriteLine("Opción no válida. Intente nuevamente.");
        Pausar();
        opcionPrincipal = -1;
    }
}

// Obtener la tablas selecionada.
Dictionary<int, Dictionary<string, string>> ObtenerTabla(string nombre)
{
    return nombre switch
    {
        "Deportes" => Deportes,
        "Categorias" => Categorias,
        "Equipos" => Equipos,
        "Posiciones" => Posiciones,
        "Jugadores" => Jugadores,
        "EquipoJugador" => EquipoJugador,
        "Entrenadores" => Entrenadores,
        "EquipoEntrenador" => EquipoEntrenador,
        "Temporadas" => Temporadas,
        "Partidos" => Partidos,
        _ => throw new ArgumentException("Tabla desconocida.")
    };
}

// Menu de cada tambla.
void GestionarTabla(Dictionary<int, Dictionary<string, string>> tabla, string nombre)
{
    int opcion = -1;

    while (opcion != 0)
    {
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine("          GESTIÓN DE " + nombre.ToUpper());
        Console.WriteLine("========================================");
        Console.WriteLine("1. Agregar");
        Console.WriteLine("2. Buscar");
        Console.WriteLine("3. Modificar");
        Console.WriteLine("4. Eliminar");
        Console.WriteLine("5. Listar");
        Console.WriteLine("0. Volver al menú principal");
        Console.WriteLine("========================================");
        Console.Write("Seleccione: ");

        if (!int.TryParse(Console.ReadLine(), out opcion))
        {
            Console.WriteLine("Debe escribir un número.");
            Pausar();
            opcion = -1;
            continue;
        }

        switch (opcion)
        {
            case 1:
                Agregar(tabla, nombre);
                break;

            case 2:
                Buscar(tabla, nombre);
                break;

            case 3:
                Modificar(tabla, nombre);
                break;

            case 4:
                Eliminar(tabla, nombre);
                break;

            case 5:
                Listar(tabla, nombre);
                break;

            case 0:
                break;

            default:
                Console.WriteLine("Opción incorrecta. Intente nuevamente.");
                Pausar();
                opcion = -1;
                break;
        }
    }
}

// Guarda temporarmente los datos del sistema temporarmente,
// Hasta que se cierre el sistema por completo.
Dictionary<string, string>? CapturarDatos(
    string nombre,
    Dictionary<string, string>? actual = null)
{
    Dictionary<string, string> datos = actual == null
        ? new Dictionary<string, string>()
        : new Dictionary<string, string>(actual);

    foreach (string campo in campos[nombre])
    {
        string anterior = actual != null && actual.ContainsKey(campo)
            ? actual[campo]
            : "";

        Console.Write($"{campo}{(anterior != "" ? " [" + anterior + "]" : "")}: ");
        string valor = (Console.ReadLine() ?? "").Trim();

        // V permite cancelar y regresar.
        if (valor.Equals("V", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Operación cancelada.");
            return null;
        }

        // ENTER conserva el valor anterior al modificar.
        if (actual != null && valor == "")
            valor = anterior;

        bool fechaFinalTemporada =
            nombre == "Temporadas" && campo == "FechaFin";

        if (valor == "" && opcionales.Contains(campo) && !fechaFinalTemporada)
        {
            datos[campo] = "";
            continue;
        }

        // Valores predeterminados.
        if (valor == "" && campo == "Estado")
        {
            valor = nombre switch
            {
                "Deportes" => "1",
                "Temporadas" => "Planificada",
                "Partidos" => "Programado",
                _ => "Activo"
            };
        }

        if (valor == "" && campo == "FechaInicio")
            valor = DateTime.Today.ToString("yyyy-MM-dd");

        if (valor == "")
        {
            Console.WriteLine("Este campo es obligatorio.");
            Pausar();
            return null;
        }

        if (!ValidarFormato(campo, valor))
        {
            Console.WriteLine("El formato introducido no es válido.");
            Pausar();
            return null;
        }

        if (!ValidarRelacion(campo, valor))
        {
            Console.WriteLine("No existe el registro relacionado indicado.");
            Pausar();
            return null;
        }

        datos[campo] = valor;
    }

    if (!ValidarReglas(nombre, datos, actual))
        return null;

    return datos;
}

// Valida formato.
bool ValidarFormato(string campo, string valor)
{
    if (camposNumericos.Contains(campo))
    {
        if (!int.TryParse(valor, out int numero) || numero < 0)
            return false;

        if (campo == "Dorsal" && (numero < 1 || numero > 99))
            return false;

        return true;
    }

    if (camposFecha.Contains(campo))
        return DateTime.TryParse(valor, out _);

    return true;
}

// Valida las relaciones entre tablas.
bool ValidarRelacion(string campo, string valor)
{
    switch (campo)
    {
        case "IdDeporte":
            return int.TryParse(valor, out int deporte)
                && Deportes.ContainsKey(deporte);

        case "IdCategoria":
            return int.TryParse(valor, out int categoria)
                && Categorias.ContainsKey(categoria);

        case "IdEquipo":
        case "IdEquipoLocal":
        case "IdEquipoVisitante":
            return int.TryParse(valor, out int equipo)
                && Equipos.ContainsKey(equipo);

        case "IdJugador":
            return int.TryParse(valor, out int jugador)
                && Jugadores.ContainsKey(jugador);

        case "IdPosicion":
            return int.TryParse(valor, out int posicion)
                && Posiciones.ContainsKey(posicion);

        case "IdEntrenador":
            return int.TryParse(valor, out int entrenador)
                && Entrenadores.ContainsKey(entrenador);

        case "IdTemporada":
            return int.TryParse(valor, out int temporada)
                && Temporadas.ContainsKey(temporada);

        default:
            return true;
    }
}

// Reglas del sistema.
bool ValidarReglas(
    string nombre,
    Dictionary<string, string> d,
    Dictionary<string, string>? anterior = null)
{
    if (nombre == "Categorias" &&
        int.Parse(d["EdadMaxima"]) < int.Parse(d["EdadMinima"]))
    {
        Console.WriteLine("La edad máxima debe ser mayor o igual a la mínima.");
        return false;
    }

    if (nombre == "Equipos")
    {
        bool repetido = Equipos.Values.Any(x =>
            x != anterior &&
            x["Nombre"].Equals(d["Nombre"], StringComparison.OrdinalIgnoreCase) &&
            x["IdDeporte"] == d["IdDeporte"] &&
            x["IdCategoria"] == d["IdCategoria"]);

        if (repetido)
        {
            Console.WriteLine("Ese equipo ya está registrado.");
            return false;
        }
    }

    if (nombre == "Posiciones")
    {
        bool repetido = Posiciones.Values.Any(x =>
            x != anterior &&
            x["Nombre"].Equals(d["Nombre"], StringComparison.OrdinalIgnoreCase) &&
            x["IdDeporte"] == d["IdDeporte"]);

        if (repetido)
        {
            Console.WriteLine("Esa posición ya está registrada.");
            return false;
        }
    }

    if (nombre == "EquipoJugador")
    {
        bool jugadorOcupado = EquipoJugador.Values.Any(x =>
            x != anterior &&
            x["IdJugador"] == d["IdJugador"] &&
            x["FechaFin"] == "");

        if (d["FechaFin"] == "" && jugadorOcupado)
        {
            Console.WriteLine("El jugador ya pertenece a un equipo activo.");
            return false;
        }

        if (!Posiciones.TryGetValue(int.Parse(d["IdPosicion"]), out var pos) ||
            !Equipos.TryGetValue(int.Parse(d["IdEquipo"]), out var eq) ||
            pos["IdDeporte"] != eq["IdDeporte"])
        {
            Console.WriteLine("La posición no corresponde al deporte del equipo.");
            return false;
        }

        if (d["Dorsal"] != "" && d["FechaFin"] == "")
        {
            bool dorsalOcupado = EquipoJugador.Values.Any(x =>
                x != anterior &&
                x["IdEquipo"] == d["IdEquipo"] &&
                x["Dorsal"] == d["Dorsal"] &&
                x["FechaFin"] == "");

            if (dorsalOcupado)
            {
                Console.WriteLine("Ese dorsal ya está ocupado en el equipo.");
                return false;
            }
        }
    }

    if (nombre == "EquipoEntrenador")
    {
        bool ocupado = EquipoEntrenador.Values.Any(x =>
            x != anterior &&
            x["IdEntrenador"] == d["IdEntrenador"] &&
            x["FechaFin"] == "");

        if (d["FechaFin"] == "" && ocupado)
        {
            Console.WriteLine("El entrenador ya tiene una asignación activa.");
            return false;
        }
    }

    if (nombre == "Temporadas")
    {
        if (string.IsNullOrWhiteSpace(d["FechaFin"]))
        {
            Console.WriteLine("La fecha final es obligatoria.");
            return false;
        }

        if (DateTime.Parse(d["FechaFin"]) < DateTime.Parse(d["FechaInicio"]))
        {
            Console.WriteLine("La fecha final no puede ser anterior a la inicial.");
            return false;
        }
    }

    if ((nombre == "EquipoJugador" || nombre == "EquipoEntrenador") &&
        d["FechaFin"] != "" &&
        DateTime.Parse(d["FechaFin"]) < DateTime.Parse(d["FechaInicio"]))
    {
        Console.WriteLine("La fecha final no puede ser anterior a la inicial.");
        return false;
    }

    if (nombre == "Deportes" && Deportes.Values.Any(x =>
        x != anterior &&
        x["Nombre"].Equals(d["Nombre"], StringComparison.OrdinalIgnoreCase)))
    {
        Console.WriteLine("Ese deporte ya está registrado.");
        return false;
    }

    if (nombre == "Categorias" && Categorias.Values.Any(x =>
        x != anterior &&
        x["Nombre"].Equals(d["Nombre"], StringComparison.OrdinalIgnoreCase)))
    {
        Console.WriteLine("Esa categoría ya está registrada.");
        return false;
    }

    if (nombre == "Temporadas" && Temporadas.Values.Any(x =>
        x != anterior &&
        x["Nombre"].Equals(d["Nombre"], StringComparison.OrdinalIgnoreCase)))
    {
        Console.WriteLine("Esa temporada ya está registrada.");
        return false;
    }

    if (nombre == "Jugadores" && d["Documento"] != "" &&
        Jugadores.Values.Any(x => x != anterior && x["Documento"] == d["Documento"]))
    {
        Console.WriteLine("Ese documento ya está registrado.");
        return false;
    }

    if (nombre == "Entrenadores" && d["Correo"] != "" &&
        Entrenadores.Values.Any(x => x != anterior && x["Correo"] == d["Correo"]))
    {
        Console.WriteLine("Ese correo ya está registrado.");
        return false;
    }

    if (nombre == "Partidos")
    {
        if (d["IdEquipoLocal"] == d["IdEquipoVisitante"])
        {
            Console.WriteLine("Los equipos deben ser diferentes.");
            return false;
        }

        if ((d["GolesLocal"] != "" && d["GolesVisitante"] == "") ||
            (d["GolesLocal"] == "" && d["GolesVisitante"] != ""))
        {
            Console.WriteLine("Debe introducir ambos resultados o dejar ambos vacíos.");
            return false;
        }

        if (d["Estado"] == "Finalizado" &&
            (d["GolesLocal"] == "" || d["GolesVisitante"] == ""))
        {
            Console.WriteLine("Un partido finalizado debe tener resultados.");
            return false;
        }
    }

    return true;
}

// AGREGAR REGISTROS.
void Agregar(Dictionary<int, Dictionary<string, string>> tabla, string nombre)
{
    Console.Clear();
    Console.WriteLine("AGREGAR " + nombre.ToUpper());
    Console.WriteLine("Escriba V en cualquier campo para cancelar.");
    Console.WriteLine("----------------------------------------");

    var datos = CapturarDatos(nombre);

    if (datos == null)
    {
        Console.WriteLine("No se guardaron los datos.");
        Pausar();
        return;
    }

    int id = tabla.Count == 0 ? 1 : tabla.Keys.Max() + 1;
    tabla.Add(id, datos);

    Console.WriteLine($"Registro agregado correctamente. ID: {id}");
    Pausar();
}

// Busca los registro.
void Buscar(Dictionary<int, Dictionary<string, string>> tabla, string nombre)
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("BUSCAR EN " + nombre.ToUpper());
        Console.WriteLine("0. Volver al menú anterior");
        Console.WriteLine("1. Buscar por ID");

        for (int i = 0; i < campos[nombre].Length; i++)
            Console.WriteLine($"{i + 2}. Buscar por {campos[nombre][i]}");

        Console.Write("\nSeleccione el criterio: ");

        if (!int.TryParse(Console.ReadLine(), out int criterio) ||
            criterio < 0 || criterio > campos[nombre].Length + 1)
        {
            Console.WriteLine("Opción incorrecta. Intente nuevamente.");
            Pausar();
            continue;
        }

        if (criterio == 0)
            return;

        Console.Write("Escriba el valor que desea buscar: ");
        string valor = (Console.ReadLine() ?? "").Trim();

        if (valor.Equals("V", StringComparison.OrdinalIgnoreCase))
            continue;

        bool encontrado = false;

        foreach (var registro in tabla)
        {
            bool coincide = criterio == 1
                ? registro.Key.ToString() == valor
                : registro.Value[campos[nombre][criterio - 2]]
                    .Contains(valor, StringComparison.OrdinalIgnoreCase);

            if (coincide)
            {
                MostrarRegistro(registro.Key, registro.Value);
                encontrado = true;
            }
        }

        if (!encontrado)
            Console.WriteLine("No se encontraron registros.");

        Pausar();
        return;
    }
}

// Modifica registro.
void Modificar(Dictionary<int, Dictionary<string, string>> tabla, string nombre)
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("MODIFICAR " + nombre.ToUpper());
        Console.WriteLine("0. Volver al menú anterior");
        Console.Write("Ingrese el ID del registro: ");

        string entrada = (Console.ReadLine() ?? "").Trim();

        if (entrada == "0" || entrada.Equals("V", StringComparison.OrdinalIgnoreCase))
            return;

        if (!int.TryParse(entrada, out int id) || !tabla.ContainsKey(id))
        {
            Console.WriteLine("ID incorrecto. Intente nuevamente.");
            Pausar();
            continue;
        }

        MostrarRegistro(id, tabla[id]);

        Console.WriteLine("\nSeleccione el campo que desea modificar:");
        Console.WriteLine("0. Volver al menú anterior");

        for (int i = 0; i < campos[nombre].Length; i++)
            Console.WriteLine($"{i + 1}. {campos[nombre][i]}");

        Console.Write("Opción: ");

        if (!int.TryParse(Console.ReadLine(), out int opcion) ||
            opcion < 0 || opcion > campos[nombre].Length)
        {
            Console.WriteLine("Opción incorrecta.");
            Pausar();
            continue;
        }

        if (opcion == 0)
            return;

        string campo = campos[nombre][opcion - 1];

        Console.Write($"Nuevo valor para {campo} (V para cancelar): ");
        string valor = (Console.ReadLine() ?? "").Trim();

        if (valor.Equals("V", StringComparison.OrdinalIgnoreCase))
            return;

        if (valor == "" && !opcionales.Contains(campo))
        {
            Console.WriteLine("El campo no puede quedar vacío.");
            Pausar();
            continue;
        }

        if (nombre == "Temporadas" && campo == "FechaFin" && valor == "")
        {
            Console.WriteLine("La fecha final es obligatoria.");
            Pausar();
            continue;
        }

        if (valor != "" && !ValidarFormato(campo, valor))
        {
            Console.WriteLine("Formato incorrecto.");
            Pausar();
            continue;
        }

        if (valor != "" && !ValidarRelacion(campo, valor))
        {
            Console.WriteLine("El registro relacionado no existe.");
            Pausar();
            continue;
        }

        var copia = new Dictionary<string, string>(tabla[id]);
        copia[campo] = valor;

        if (!ValidarReglas(nombre, copia, tabla[id]))
        {
            Pausar();
            continue;
        }

        tabla[id] = copia;

        Console.WriteLine("Registro modificado correctamente.");
        Pausar();
        return;
    }
}

// Elimina registro.
void Eliminar(Dictionary<int, Dictionary<string, string>> tabla, string nombre)
{
    Console.Clear();
    Console.WriteLine("ELIMINAR " + nombre.ToUpper());
    Console.WriteLine("Escriba 0 para volver al menú anterior.");
    Console.Write("Ingrese el ID: ");

    string entrada = (Console.ReadLine() ?? "").Trim();

    if (entrada == "0" || entrada.Equals("V", StringComparison.OrdinalIgnoreCase))
        return;

    if (!int.TryParse(entrada, out int id) || !tabla.ContainsKey(id))
    {
        Console.WriteLine("No existe un registro con ese ID.");
        Pausar();
        return;
    }

    bool relacionado = false;

    switch (nombre)
    {
        case "Deportes":
            relacionado = Equipos.Values.Any(x => x["IdDeporte"] == id.ToString()) ||
                          Posiciones.Values.Any(x => x["IdDeporte"] == id.ToString());
            break;

        case "Categorias":
            relacionado = Equipos.Values.Any(x => x["IdCategoria"] == id.ToString());
            break;

        case "Equipos":
            relacionado = EquipoJugador.Values.Any(x => x["IdEquipo"] == id.ToString()) ||
                          EquipoEntrenador.Values.Any(x => x["IdEquipo"] == id.ToString()) ||
                          Partidos.Values.Any(x =>
                              x["IdEquipoLocal"] == id.ToString() ||
                              x["IdEquipoVisitante"] == id.ToString());
            break;

        case "Posiciones":
            relacionado = EquipoJugador.Values.Any(x => x["IdPosicion"] == id.ToString());
            break;

        case "Jugadores":
            relacionado = EquipoJugador.Values.Any(x => x["IdJugador"] == id.ToString());
            break;

        case "Entrenadores":
            relacionado = EquipoEntrenador.Values.Any(x => x["IdEntrenador"] == id.ToString());
            break;

        case "Temporadas":
            relacionado = Partidos.Values.Any(x => x["IdTemporada"] == id.ToString());
            break;
    }

    if (relacionado)
    {
        Console.WriteLine("No se puede eliminar porque existen registros relacionados.");
        Pausar();
        return;
    }

    MostrarRegistro(id, tabla[id]);

    Console.Write("\n¿Confirma la eliminación? (S/N): ");
    string respuesta = (Console.ReadLine() ?? "").Trim();

    if (respuesta.Equals("S", StringComparison.OrdinalIgnoreCase))
    {
        tabla.Remove(id);
        Console.WriteLine("Registro eliminado correctamente.");
    }
    else
    {
        Console.WriteLine("Eliminación cancelada.");
    }

    Pausar();
}

// Lista de registro.
void Listar(Dictionary<int, Dictionary<string, string>> tabla, string nombre)
{
    Console.Clear();
    Console.WriteLine("LISTADO DE " + nombre.ToUpper());
    Console.WriteLine("========================================");

    if (tabla.Count == 0)
    {
        Console.WriteLine("No hay registros almacenados.");
    }
    else
    {
        foreach (var registro in tabla)
        {
            MostrarRegistro(registro.Key, registro.Value);
            Console.WriteLine("----------------------------------------");
        }

        Console.WriteLine($"Total de registros: {tabla.Count}");
    }

    Pausar();
}

// Muestra los datos de un registrio. :V
void MostrarRegistro(int id, Dictionary<string, string> datos)
{
    Console.WriteLine($"\nID: {id}");

    foreach (var dato in datos)
        Console.WriteLine($"{dato.Key}: {(dato.Value == "" ? "(vacío)" : dato.Value)}");
}

// Pausa el sistema....
void Pausar()
{
    Console.WriteLine("\nPresione ENTER para continuar...");
    Console.ReadLine();
}
