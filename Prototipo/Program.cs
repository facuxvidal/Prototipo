
Console.WriteLine("------------------------------------\nBIENVENIDO\n------------------------------------");

bool login = verifica_logueo();
if (login)
{
    string rsp_principal;
    string rsp_peso;
    string rsp_resumen;
    string region_origen;
    string region_destino;
    string direccion_origen;
    string direccion_destino;
    string origen;
    string destino;
    //string rsp_ciudad_origen;
    Console.WriteLine("------------------------------------\nUSTED ES CLIENTE CORPORATIVO");

    rsp_principal = menu_principal();
    if (rsp_principal == "1") //Solicitar servicio
    {
        rsp_peso = consulta_peso_paquete();
        List<string> posibles_respuestas_validas_peso = new List<string>();
        posibles_respuestas_validas_peso.Add("1");
        posibles_respuestas_validas_peso.Add("2");
        posibles_respuestas_validas_peso.Add("3");
        posibles_respuestas_validas_peso.Add("4");
        if (posibles_respuestas_validas_peso.Contains(rsp_peso))
        {
            Console.WriteLine("------------------------------------\nDATOS DEL CLIENTE");
            Console.WriteLine("Nombre: JUAN | Apellido: PEREZ | Teléfono: 011-1512345678 | DNI: 12345678 | Correo Electrónico: admin@admin.com");
            region_origen = consulta_region_origen();
            direccion_origen = consulta_direccion("origen");
            origen = $"{direccion_origen}, {region_origen}";
            region_destino = consulta_region_destino();
            direccion_destino = consulta_direccion("destino");
            destino = $"{direccion_destino}, {region_destino}";
            
            muestra_resumen_pedido(origen, destino);
            Console.WriteLine("Muchas gracias por utilizar nuestra aplicación! Esperamos verlo pronto!");
            Console.ReadLine();
        }
        else // Aca seleccionó la opcion [5] de la consulta del peso
        {
            Console.WriteLine("Lo sentimos pero nuestro servicio solo admite paquetes menores a 30 kg. Hasta luego!");
            Console.ReadLine();
        }
    }
    else if (rsp_principal == "2") //Consultar estado de cuenta
    {
        Console.WriteLine($"Usted marcó la opcion [{rsp_principal}]");
        Console.ReadLine();
    }
    else if(rsp_principal == "3") //Consultar seguimiento de pedido
    {
        Console.WriteLine($"Usted marcó la opcion [{rsp_principal}]");
        Console.ReadLine();
    }
    else if (rsp_principal == "4") //Salir
    {
        Console.WriteLine($"Usted marcó la opcion [{rsp_principal}]");
        Console.WriteLine("Hasta luego!");
        Console.ReadLine();
    }
}
else
{
    Console.WriteLine("------------------------------------\nNo se pudieron verificar sus credenciales de ingreso! Presiona [Enter] para salir.");
    Console.ReadLine();
}




bool verifica_logueo()
{
    string usuario;
    string contraseña;
    Console.WriteLine("Inicie sesion con su usuario y contraseña");
    Console.Write("Usuario: ");
    usuario = Console.ReadLine().Trim();
    Console.Write("Contraseña: ");
    contraseña = Console.ReadLine().Trim();

    bool rsp = true;
    if (usuario != "admin")
    {
        Console.WriteLine("------------------------------------\nERROR - Usuario Inexistente");
        rsp = false;
    }
    if (contraseña != "admin")
    {
        Console.WriteLine("------------------------------------\nERROR - Contraseña erronea");
        rsp = false;
    }
    return rsp;
}


string menu_principal()
{
    List<string> opciones_validas = new List<string>();
    opciones_validas.Add("1");
    opciones_validas.Add("2");
    opciones_validas.Add("3");
    opciones_validas.Add("4");

    string opcion_elegida = null;
    bool bandera = true;
    while (bandera)
    {
        Console.WriteLine("------------------------------------\nIngrese un numero de acuerdo a la accion que necesita realizar");
        Console.WriteLine("[1] Solicitar servicio \n[2] Consultar estado de cuenta \n[3] Consultar seguimiento del pedido \n[4] Salir");
        opcion_elegida = Console.ReadLine();

        if (String.IsNullOrEmpty(opcion_elegida))
        {
            Console.WriteLine("------------------------------------\nERROR - No seleccionó ninguna opcion. Por favor seleccione una opcion!");
            Console.WriteLine("------------------------------------\nIntente nuevamente!");
        }
        else if (!valida_entero(opcion_elegida))
        {
            Console.WriteLine("------------------------------------\nERROR - No se pudo validar el numero ingresado!");
            Console.WriteLine("------------------------------------\nIntente nuevamente!");
        }
        else if (!opciones_validas.Contains(opcion_elegida))
        {
            Console.WriteLine("------------------------------------\nERROR - Marcó una opcion fuera del intervalo propuesto!");
            Console.WriteLine("------------------------------------\nIntente nuevamente!");
        }
        else
        {
            bandera = false;
        }

    }

    return opcion_elegida;
}


bool valida_entero(string entero_a_validar)
{
    bool rsp = true;
    int entero_validado;
    try
    {
        entero_validado = Convert.ToInt32(entero_a_validar);
    }
    catch (Exception e)
    {
        rsp = false;
    }
    return rsp;
}


string consulta_peso_paquete()
{
    List<string> opciones_validas = new List<string>();
    opciones_validas.Add("1");
    opciones_validas.Add("2");
    opciones_validas.Add("3");
    opciones_validas.Add("4");
    opciones_validas.Add("5");

    string opcion_elegida = null;
    bool bandera = true;
    while (bandera)
    {
        Console.WriteLine("------------------------------------\nIngrese un numero de acuerdo a la opcion que corresponda\n------------------------------------");
        Console.WriteLine("[1] Menor o igual a 500gr \n[2] Mayor a 500gr y hasta 10 kg \n[3] Hasta 20 kg \n[4] Hasta 30 kg \n[5] Mayor");
        opcion_elegida = Console.ReadLine();

        if (String.IsNullOrEmpty(opcion_elegida))
        {
            Console.WriteLine("------------------------------------\nERROR - No seleccionó ninguna opcion. Por favor seleccione una opcion!");
            Console.WriteLine("------------------------------------\nIntente nuevamente!");
        }
        else if (!valida_entero(opcion_elegida))
        {
            Console.WriteLine("------------------------------------\nERROR - No se pudo validar el numero ingresado!");
            Console.WriteLine("------------------------------------\nIntente nuevamente!");
        }
        else if (!opciones_validas.Contains(opcion_elegida))
        {
            Console.WriteLine("------------------------------------\nERROR - Marcó una opcion fuera del intervalo propuesto!");
            Console.WriteLine("------------------------------------\nIntente nuevamente!");
        }
        else
        {
            bandera = false;
        }
    }

    return opcion_elegida;
}


string consulta_region_origen()
{
    List<string> opciones_validas = new List<string>();
    opciones_validas.Add("1");
    opciones_validas.Add("2");
    opciones_validas.Add("3");
    opciones_validas.Add("4");

    string opcion_elegida = null;
    bool bandera = true;

    while (bandera)
    {
        Console.WriteLine("------------------------------------\nIngrese un numero de acuerdo a la opcion que corresponda\n------------------------------------");
        Console.WriteLine("[1] VIEDMA \n[2] CORDOBA \n[3] RESISTENCIA \n[4] CABA/BUENOS AIRES");
        opcion_elegida = Console.ReadLine();

        if (String.IsNullOrEmpty(opcion_elegida))
        {
            Console.WriteLine("------------------------------------\nERROR - No seleccionó ninguna opcion. Por favor seleccione una opcion!");
            Console.WriteLine("------------------------------------\nIntente nuevamente!");
        }
        else if (!valida_entero(opcion_elegida))
        {
            Console.WriteLine("------------------------------------\nERROR - No se pudo validar el numero ingresado!");
            Console.WriteLine("------------------------------------\nIntente nuevamente!");
        }
        else if (!opciones_validas.Contains(opcion_elegida))
        {
            Console.WriteLine("------------------------------------\nERROR - Marcó una opcion fuera del intervalo propuesto!");
            Console.WriteLine("------------------------------------\nIntente nuevamente!");
        }
        else
        {
            bandera = false;
        }
    }

    string rsp;
    switch (opcion_elegida)
    {
        case "1":
            {
                rsp = "VIEDMA";
                break;
            }
        case "2":
            {
                rsp = "CORDOBA";
                break;
            }
        case "3":
            {
                rsp = "RESISTENCIA";
                break;
            }
        case "4":
            {
                rsp = "CABA / BUENOS AIRES";
                break;
            }
        default:
            rsp = "Sin Identificar";
            break;
    }
    return rsp;
}


string consulta_region_destino()
{
    List<string> opciones_validas = new List<string>();
    opciones_validas.Add("1");
    opciones_validas.Add("2");
    opciones_validas.Add("3");
    opciones_validas.Add("4");
    opciones_validas.Add("5");
    opciones_validas.Add("6");

    string opcion_elegida = null;
    bool bandera = true;

    while (bandera)
    {
        Console.WriteLine("------------------------------------\nIngrese un numero de acuerdo a la opcion que corresponda\n------------------------------------");
        Console.WriteLine("[1] PAÍSES LIMÍTROFES \n[2] RESTO DE AMÉRICA LATINA \n[3] AMÉRICA DEL NORTE \n[4] EUROPA \n[5] ASIA \n[6] ARGENTINA ");
        opcion_elegida = Console.ReadLine();

        if (String.IsNullOrEmpty(opcion_elegida))
        {
            Console.WriteLine("------------------------------------\nERROR - No seleccionó ninguna opcion. Por favor seleccione una opcion!");
            Console.WriteLine("------------------------------------\nIntente nuevamente!");
        }
        else if (!valida_entero(opcion_elegida))
        {
            Console.WriteLine("------------------------------------\nERROR - No se pudo validar el numero ingresado!");
            Console.WriteLine("------------------------------------\nIntente nuevamente!");
        }
        else if (!opciones_validas.Contains(opcion_elegida))
        {
            Console.WriteLine("------------------------------------\nERROR - Marcó una opcion fuera del intervalo propuesto!");
            Console.WriteLine("------------------------------------\nIntente nuevamente!");
        }
        else
        {
            bandera = false;
        }
    }
    string rsp;
    switch (opcion_elegida)
    {
        case "1":
            {
                rsp = "PAÍSES LIMÍTROFES";
                break;
            }
        case "2":
            {
                rsp = "RESTO DE AMÉRICA LATINA";
                break;
            }
        case "3":
            {
                rsp = "AMÉRICA DEL NORTE";
                break;
            }
        case "4":
            {
                rsp = "EUROPA";
                break;
            }
        case "5":
            {
                rsp = "ASIA";
                break;
            }
        case "6":
            {
                rsp = "ARGENTINA";
                break;
            }
        default:
            rsp = "Sin Identificar";
            break;
    }
    return rsp;
}


string consulta_direccion(string tipo_de_direccion)
{
    string direccion_origen = "";
    bool bandera = true;
    while (bandera)
    {
        Console.WriteLine($"------------------------------------\nIngrese su dirección de {tipo_de_direccion}/sucursal: Calle, Altura, Departamento y Código Postal:");
        direccion_origen = Console.ReadLine().Trim();
        if (String.IsNullOrEmpty(direccion_origen))
        {
            Console.WriteLine("------------------------------------\nERROR - Deberá aclarar una direccion valida!");
            Console.WriteLine("------------------------------------\nIntente nuevamente!");
        }
        else
        {
            bandera = false;
        }
    }
    return direccion_origen;
}


void muestra_resumen_pedido(string origen, string destino)
{
    Console.WriteLine("------------------------------------\nRESUMEN DEL PEDIDO");
    Console.WriteLine($"Producto: Smart TV 50' 4K UHD Philips | Tarifa: $100.000 | Origen: {origen} | Destino : {destino}");

    List<string> opciones_validas = new List<string>();
    opciones_validas.Add("1");
    opciones_validas.Add("2");

    string opcion_elegida = null;
    bool bandera = true;
    while (bandera)
    {
        Console.WriteLine("------------------------------------\nPor favor confirmar el pedido");
        Console.WriteLine("[1] Confirmar \n[2] Cancelar");
        opcion_elegida = Console.ReadLine();

        if (String.IsNullOrEmpty(opcion_elegida))
        {
            Console.WriteLine("------------------------------------\nERROR - No seleccionó ninguna opcion. Por favor seleccione una opcion!");
            Console.WriteLine("------------------------------------\nIntente nuevamente!");
        }
        else if (!valida_entero(opcion_elegida))
        {
            Console.WriteLine("------------------------------------\nERROR - No se pudo validar el numero ingresado!");
            Console.WriteLine("------------------------------------\nIntente nuevamente!");
        }
        else if (!opciones_validas.Contains(opcion_elegida))
        {
            Console.WriteLine("------------------------------------\nERROR - Marcó una opcion fuera del intervalo propuesto!");
            Console.WriteLine("------------------------------------\nIntente nuevamente!");
        }
        else
        {
            bandera = false;
        }

    }
}


public static class VariablesGlobales
{
    public static List<string> OPCIONES_VALIDAS = new List<string>();
}
