using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;

Console.WriteLine("------------------------------------\nBIENVENIDO\n------------------------------------");

bool login = verifica_logueo();
if (login)
{
    string rsp_principal;
    string rsp_peso;
    string region_origen;
    string region_destino;
    string direccion_origen;
    string direccion_destino;
    string origen;
    string destino;
    string rsp_estado_de_cuenta;
    string provincia_destino;
    Dictionary<int, List<string>> orden = null;
    string retiro_o_entrega;
    string sucursal_de_retiro;
    bool urgente;
    Console.WriteLine("------------------------------------\nUSTED ES CLIENTE CORPORATIVO");

    rsp_principal = menu_principal();
    if (rsp_principal == "1") //Solicitar servicio
    {
        bool bandera_bultos = true;
        do
        {
            Console.WriteLine("------------------------------------\nIngrese la cantidad de Encomiendas o Correspondencia que desee enviar en su pedido: ");
            string encomiendas = Console.ReadLine();
            if (valida_entero(encomiendas))
            {
                int contador_encomiendas = 0;
                bool retiro_domicilio, entrega_domicilio;
                do
                {
                    rsp_peso = consulta_peso_encomienda(contador_encomiendas);
                    List<string> posibles_respuestas_validas_peso = new List<string>();
                    posibles_respuestas_validas_peso.Add("1");
                    posibles_respuestas_validas_peso.Add("2");
                    posibles_respuestas_validas_peso.Add("3");
                    posibles_respuestas_validas_peso.Add("4");

                    if (posibles_respuestas_validas_peso.Contains(rsp_peso))
                    {
                        contador_encomiendas++;
                    }
                    else
                    {
                        Console.WriteLine("------------------------------------\nLo sentimos pero nuestro servicio solo admite encomiendas menores a 30 kg.");
                    }

                } while (Convert.ToInt32(encomiendas) != contador_encomiendas);

                Console.WriteLine("------------------------------------\nDATOS DEL CLIENTE");
                Console.WriteLine("Nombre: JUAN | Apellido: PEREZ | Teléfono: 011-1512345678 | DNI: 12345678 | Correo Electrónico: admin@admin.com");

                urgente = consulta_urgencia();

                // Consulta direcion de entrega o retiro ORIGEN
                region_origen = consulta_region("origen");
                retiro_o_entrega = consulta_retiro();
                if (retiro_o_entrega == "Recoleccion del Domicilio")
                {
                    direccion_origen = consulta_direccion_nacional("origen");
                    origen = $"{direccion_origen}, {region_origen}";
                    entrega_domicilio = true;
                }
                else
                {
                    sucursal_de_retiro = consulto_sucursales();
                    origen = $"{sucursal_de_retiro}";
                    entrega_domicilio = false;
                }

                // Consulta direcion de entrega o retiro DESTINO
                region_destino = consulta_intenacional_destino();
                
                if (region_destino == "Argentina")
                {

                    retiro_o_entrega = consulta_entrega();
                    if (retiro_o_entrega == "Entrega a Domicilio")
                    {
                        provincia_destino = consulta_region("destino");
                        direccion_destino = consulta_direccion_nacional("destino");
                        destino = $"{direccion_destino}, {provincia_destino}, {region_destino}";
                        retiro_domicilio = true;
                    }
                    else
                    {
                        provincia_destino = consulta_region("destino");
                        sucursal_de_retiro = consulto_sucursales();
                        destino = $"{sucursal_de_retiro}, en {provincia_destino}";
                        retiro_domicilio = false;
                    }
                }
                else
                {

                    direccion_destino = consulta_direccion_internacional("destino");
                    
                    destino = $"{direccion_destino}, {region_destino}";
                    retiro_domicilio = true;
                }

                muestra_resumen_pedido(origen, destino, Convert.ToInt32(encomiendas), urgente, retiro_domicilio, entrega_domicilio, region_destino);

                // Mostramos saldo de cuenta corporativa al realizar el pedido 
                Console.WriteLine("------------------------------------\nSALDO DE SU CUENTA");
                Console.WriteLine("El saldo final de su Estado de cuenta es: $54.340");

                Console.WriteLine("Muchas gracias por utilizar nuestra aplicación! Esperamos verlo pronto!");
                Console.WriteLine("Presione [Enter] para salir");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("------------------------------------\nERROR - No se pudo validar el numero ingresado!");
                bandera_bultos = false;
            }
        } while (!bandera_bultos);
    }
    else if (rsp_principal == "2") //Consultar estado de cuenta
    {
        rsp_estado_de_cuenta = consulta_estado_de_cuenta();
        if (rsp_estado_de_cuenta == "1")
        {
            Console.WriteLine("------------------------------------\nDATOS DE LA FACTURACION\n------------------------------------");
            Console.WriteLine("Servicio N°: 823053 \nTarifa: $100.000 \nFecha Desde: 16/10/2022 \nFecha Hasta: 16/11/2022 \nEstado: IMPAGA");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Servicio N°: 100235 \nTarifa: $23.500 \nFecha Desde: 02/11/2022 \nFecha Hasta: 02/12/2022 \nEstado: PAGA");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Precio Total para la Factura N°0001-452834024: $123.500");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Muchas gracias por utilizar nuestra aplicación! Esperamos verlo pronto!");
            Console.WriteLine("Presione [Enter] para salir");
            Console.ReadLine();
        }
        else if (rsp_estado_de_cuenta == "2")
        {
            Console.WriteLine("------------------------------------\nORDENES DE SERVICION SIN FACTURAR");
            for (int i = 0; i < 6; i++)
            {
                Random numero_random = new Random();
                Console.WriteLine($"Orden de Servicio N°{numero_random.Next()} sin facturar!");
            }
            Console.WriteLine("Muchas gracias por utilizar nuestra aplicación! Esperamos verlo pronto!");
            Console.WriteLine("Presione [Enter] para salir");
            Console.ReadLine();
        }
        else if (rsp_estado_de_cuenta == "3")
        {
            Console.WriteLine("------------------------------------\nSALDO DE SU CUENTA");
            Console.WriteLine("El saldo final de su Estado de cuenta es: $154.340");
            Console.WriteLine("Muchas gracias por utilizar nuestra aplicación! Esperamos verlo pronto!");
            Console.WriteLine("Presione [Enter] para salir");
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("Lo sentimos ocurrio un error inesperado.");
            Console.WriteLine("Muchas gracias por utilizar nuestra aplicación. Esperamos verlo pronto!");
            Console.ReadLine();
        }
    }
    else if (rsp_principal == "3") //Consultar seguimiento de pedido
    {
        orden = consulta_numero_orden();
        Console.WriteLine("------------------------------------\nDATOS DEL SEGUIMIENTO\n------------------------------------");
        Console.WriteLine($"Número de orden: {orden.First().Key} \nOrigen: {orden.First().Value[1]} \nDestino: {orden.First().Value[2]} \nTipoDeServicio: {orden.First().Value[0]}");

        Console.WriteLine($"Muchas gracias por utilizar nuestra aplicación! Esperamos verlo pronto.");
        Console.WriteLine("Presione [Enter] para salir");
        Console.ReadLine();
    }
    else if (rsp_principal == "4") //Salir
    {
        Console.WriteLine("Hasta luego!");
        Console.WriteLine("Muchas gracias por utilizar nuestra aplicación! Esperamos verlo pronto!");
        Console.WriteLine("Presione [Enter] para salir");
        Console.ReadLine();
    }
}
else
{
    Console.WriteLine("------------------------------------\nNo se pudieron verificar sus credenciales de ingreso!");
    Console.WriteLine("Muchas gracias por utilizar nuestra aplicación! Esperamos verlo pronto!");
    Console.ReadLine();
}




bool verifica_logueo()
{
    string usuario;
    string contraseña;
    bool bandera = true;
    bool rsp = false;
    while (bandera)
    {
        Console.WriteLine("Inicie sesion con su usuario y contraseña");
        Console.Write("Usuario: ");
        usuario = Console.ReadLine().Trim();
        Console.Write("Contraseña: ");
        contraseña = Console.ReadLine().Trim();

        if (usuario != "admin")
        {
            Console.WriteLine("------------------------------------\nERROR - Usuario Inexistente\n------------------------------------");
        }
        else if (contraseña != "admin")
        {
            Console.WriteLine("------------------------------------\nERROR - Contraseña erronea\n------------------------------------");
        }
        else
        {
            bandera = false;
            rsp = true;
        }
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

    string opcion_elegida = "";
    bool bandera = true;
    while (bandera)
    {
        Console.WriteLine("------------------------------------\nIngrese un numero de acuerdo a la accion que necesita realizar");
        Console.WriteLine("[1] Solicitar servicio \n[2] Consultar estado de cuenta \n[3] Consultar seguimiento del pedido \n[4] Salir");
        opcion_elegida = Console.ReadLine();

        if (String.IsNullOrEmpty(opcion_elegida))
        {
            Console.WriteLine("------------------------------------\nERROR - No seleccionó ninguna opcion.");
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

    if (!int.TryParse(entero_a_validar, out entero_validado))
    {
        rsp = false;
    }
    else if (entero_validado <= 0)
    {
        rsp = false;
    }
    return rsp;
}


string consulta_peso_encomienda(int numero_encomienda)
{
    List<string> opciones_validas = new List<string>();
    opciones_validas.Add("1");
    opciones_validas.Add("2");
    opciones_validas.Add("3");
    opciones_validas.Add("4");
    opciones_validas.Add("5");

    string opcion_elegida = "";
    bool bandera = true;
    while (bandera)
    {
        Console.WriteLine($"------------------------------------\nIngrese un numero de acuerdo al peso del encomienda/correspondencia N° {numero_encomienda + 1}\n------------------------------------");
        Console.WriteLine("[1] Menor o igual a 500gr \n[2] Mayor a 500gr y hasta 10 kg \n[3] Hasta 20 kg \n[4] Hasta 30 kg \n[5] Mayor");
        opcion_elegida = Console.ReadLine();

        if (String.IsNullOrEmpty(opcion_elegida))
        {
            Console.WriteLine("------------------------------------\nERROR - No seleccionó ninguna opcion.");
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


string consulta_region(string origen_destino)
{
    List<string> opciones_validas = new List<string>();
    opciones_validas.Add("CABA");
    opciones_validas.Add("BUENOS AIRES");
    opciones_validas.Add("CORDOBA");
    opciones_validas.Add("SAN JUAN");
    opciones_validas.Add("SAN LUIS");
    opciones_validas.Add("SANTA CRUZ");
    opciones_validas.Add("CHUBUT");
    opciones_validas.Add("RIO NEGRO");
    opciones_validas.Add("NEUQUEN");
    opciones_validas.Add("LA PAMPA");
    opciones_validas.Add("TIERRA DEL FUEGO");
    opciones_validas.Add("MENDOZA");
    opciones_validas.Add("LA RIOJA");
    opciones_validas.Add("ENTRE RIOS");
    opciones_validas.Add("SANTA FE");        
    opciones_validas.Add("CORRIENTES");
    opciones_validas.Add("MISIONES");
    opciones_validas.Add("CHACO");
    opciones_validas.Add("CATAMARCA"); 
    opciones_validas.Add("SANTIAGO DEL ESTERO");
    opciones_validas.Add("TUCUMAN");
    opciones_validas.Add("FORMOSA");
    opciones_validas.Add("SALTA");
    opciones_validas.Add("JUJUY");


    string opcion_elegida = "";
    bool bandera = true;

    while (bandera)
    {
        Console.WriteLine($"------------------------------------\nIngrese la provincia de {origen_destino} (sin tildes):  ");
        
        opcion_elegida = Console.ReadLine();

        if (String.IsNullOrEmpty(opcion_elegida))
        {
            Console.WriteLine("------------------------------------\nERROR - No ingreso ninguna provincia.");
            Console.WriteLine("------------------------------------\nIntente nuevamente!");
        }
        else if (!opciones_validas.Contains(opcion_elegida.ToUpper().Trim()))
        {
            Console.WriteLine("------------------------------------\nERROR - Ingreso una provincia inexistente!");
            Console.WriteLine("------------------------------------\nIntente nuevamente!");
        }
        else
        {
            bandera = false;
        }
    }

    return opcion_elegida;
}

string consulta_intenacional_destino()
{
    string opcion_elegida = "";
    bool flag = true;
    string rsp = "";
    List<string> opciones_validas = new List<string>();
    opciones_validas.Add("1");
    opciones_validas.Add("2");

    while (flag)
    {
        Console.WriteLine("¿Desea enviar su encomienda/correspondencia dentro de Argentina?: \n[1] SI \n[2] NO");
        opcion_elegida = Console.ReadLine();
        if (String.IsNullOrEmpty(opcion_elegida))
        {
            Console.WriteLine("------------------------------------\nERROR - No seleccionó ninguna opcion.");
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
            flag = false;
        }

     
    }
    if (opcion_elegida == "1")
    {
        rsp = "Argentina"; 
    }
    else if (opcion_elegida == "2")
    {
        bool flag2 = true;
        while (flag2)
        {
            Console.WriteLine("Ingrese el Pais de destino:  ");
            rsp = Console.ReadLine();
            if (String.IsNullOrEmpty(rsp))
            {
                Console.WriteLine("------------------------------------\nERROR - No seleccionó ninguna opcion.");
                Console.WriteLine("------------------------------------\nIntente nuevamente!");
            }
            else
            {
                flag2 = false;
            }
            
        }
    }
    return rsp;

}


string consulta_direccion_nacional(string tipo_de_direccion)
{
    string direccion_origen = "";
    bool bandera = true;
    while (bandera)
    {
        Console.WriteLine($"------------------------------------\nIngrese su dirección de {tipo_de_direccion}: Calle, Altura, Departamento y Código Postal");
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

string consulta_direccion_internacional(string tipo_de_direccion)
{
    string direccion_destino = "";
    bool bandera = true;
    while (bandera)
    {
        Console.WriteLine($"------------------------------------\nIngrese su dirección de {tipo_de_direccion}: Calle, Altura, Departamento y Ciudad ");
        direccion_destino = Console.ReadLine().Trim();
        if (String.IsNullOrEmpty(direccion_destino))
        {
            Console.WriteLine("------------------------------------\nERROR - Deberá aclarar una direccion valida!");
            Console.WriteLine("------------------------------------\nIntente nuevamente!");
        }
        else
        {
            bandera = false;
        }
    }
    return direccion_destino;
}


void muestra_resumen_pedido(string origen, string destino, int encomiendas, bool urgente, bool retiro_domicilio, bool entrega_domicilio, string es_internacional)
{
    int precio = 100000;
    Random numero_pedido = new();
    Console.WriteLine($"------------------------------------\nRESUMEN DEL PEDIDO N°{numero_pedido.Next()}");
    int adicional_urgente = 500;
    int acumulador_domicilio = 0;
    int adicional_internacional = 0;

    if (retiro_domicilio)
    {
        acumulador_domicilio += 300;
    }
    if (entrega_domicilio)
    {
        acumulador_domicilio += 300;
    }
    if (es_internacional != "Argentina")
    {
        adicional_internacional = 2000;
    }

    if (urgente)
    {
        if (encomiendas == 1)
        {
            Console.WriteLine($"Encomienda/correspondencia a enviar: {encomiendas} \nTarifa: ${(precio * encomiendas) + adicional_urgente + acumulador_domicilio + adicional_internacional} \nOrigen: {origen} \nDestino: {destino}");
        }
        else
        {
            Console.WriteLine($"Encomiendas/correspondencias a enviar: {encomiendas} \nTarifa: ${(precio * encomiendas) + adicional_urgente + acumulador_domicilio + adicional_internacional} \nOrigen: {origen} \nDestino: {destino}");
        }
    }
    else
    {
        if (encomiendas == 1)
        {
            Console.WriteLine($"Encomienda/correspondencia a enviar: {encomiendas} \nTarifa: ${(precio * encomiendas) + acumulador_domicilio + adicional_internacional} \nOrigen: {origen} \nDestino: {destino}");
        }
        else
        {
            Console.WriteLine($"Encomiendas/correspondencias a enviar: {encomiendas} \nTarifa: ${(precio * encomiendas) + acumulador_domicilio + adicional_internacional} \nOrigen: {origen} \nDestino: {destino}");
        }
    }
 

    List<string> opciones_validas = new List<string>();
    opciones_validas.Add("1");
    opciones_validas.Add("2");

    string opcion_elegida;
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


string consulta_estado_de_cuenta()
{
    List<string> opciones_validas = new List<string>();
    opciones_validas.Add("1");
    opciones_validas.Add("2");
    opciones_validas.Add("3");

    string opcion_elegida = "";
    bool bandera = true;
    while (bandera)
    {
        Console.WriteLine("------------------------------------\nIngrese un numero de acuerdo a la consulta que desea realizar");
        Console.WriteLine("[1] Facturación –paga o impaga- \n[2] Servicios cumplidos pendientes de facturación \n[3] Saldo ");
        opcion_elegida = Console.ReadLine();

        if (String.IsNullOrEmpty(opcion_elegida))
        {
            Console.WriteLine("------------------------------------\nERROR - No seleccionó ninguna opcion.");
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


Dictionary<int, List<string>> consulta_numero_orden()
{

    List<string> numeros_de_ordenes_vigentes = new List<string>();
    numeros_de_ordenes_vigentes.Add("823053");
    numeros_de_ordenes_vigentes.Add("373823");
    numeros_de_ordenes_vigentes.Add("100284");
    numeros_de_ordenes_vigentes.Add("100235");
    numeros_de_ordenes_vigentes.Add("324441");

    string numero_orden = "";
    bool bandera = true;
    while (bandera)
    {
        Console.WriteLine("------------------------------------\nIngrese el numero de orden de su pedido");
        numero_orden = Console.ReadLine();

        if (String.IsNullOrEmpty(numero_orden))
        {
            Console.WriteLine("------------------------------------\nERROR - Ingrese un número.");
            Console.WriteLine("------------------------------------\nIntente nuevamente!");
        }
        else if (!valida_entero(numero_orden))
        {
            Console.WriteLine("------------------------------------\nERROR - No se pudo validar el numero ingresado.");
            Console.WriteLine("------------------------------------\nIntente nuevamente!");
        }
        else if (!numeros_de_ordenes_vigentes.Contains(numero_orden))
        {
            Console.WriteLine("------------------------------------\nERROR - El número de orden ingresado no existe.");
            Console.WriteLine("------------------------------------\nPor favor ingrese un número de orden existente");
        }
        else
        {
            bandera = false;
        }
    }

    // Hacer que los datos cambien segun el numero de orden
    List<string> datos = new List<string>() {
        "Urgente", "La Pampa", "Chaco"
    };

    Dictionary<int, List<string>> datos_de_servicio = new Dictionary<int, List<string>>()
    {
        {int.Parse(numero_orden), datos}
    };

    return datos_de_servicio;
}


string consulta_retiro()
{
    List<string> opciones_validas = new List<string>();
    opciones_validas.Add("1");
    opciones_validas.Add("2");

    string opcion_elegida = "";
    bool bandera = true;
    while (bandera)
    {
        Console.WriteLine($"------------------------------------\nIngrese un número según la opción de entrega/retiro que le parezca mas comodo\n------------------------------------");
        Console.WriteLine("[1] Dejarlo en Sucursal \n[2] Recoleccion del Domicilio");
        opcion_elegida = Console.ReadLine();

        if (String.IsNullOrEmpty(opcion_elegida))
        {
            Console.WriteLine("------------------------------------\nERROR - No seleccionó ninguna opcion.");
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
                rsp = "Dejarlo en Sucursal";
                break;
            }
        case "2":
            {
                rsp = "Recoleccion del Domicilio";
                break;
            }
        default:
            rsp = "Sin Identificar";
            break;
    }
    return rsp;
}

string consulta_entrega()
{
    List<string> opciones_validas = new List<string>();
    opciones_validas.Add("1");
    opciones_validas.Add("2");

    string opcion_elegida = "";
    bool bandera = true;
    while (bandera)
    {
        Console.WriteLine($"------------------------------------\nIngrese un número según la opción de entrega/retiro que le parezca mas comodo\n------------------------------------");
        Console.WriteLine("[1] Retiro en Sucursal \n[2] Entrega a Domicilio");
        opcion_elegida = Console.ReadLine();

        if (String.IsNullOrEmpty(opcion_elegida))
        {
            Console.WriteLine("------------------------------------\nERROR - No seleccionó ninguna opcion.");
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
                rsp = "Retiro en Sucursal";
                break;
            }
        case "2":
            {
                rsp = "Entrega a Domicilio";
                break;
            }
        default:
            rsp = "Sin Identificar";
            break;
    }
    return rsp;
}


string consulto_sucursales()
{
    List<string> opciones_validas = new List<string>();
    opciones_validas.Add("1");
    opciones_validas.Add("2");
    opciones_validas.Add("3");
    opciones_validas.Add("4");

    string opcion_elegida = "";
    bool bandera = true;
    while (bandera)
    {
        Console.WriteLine($"------------------------------------\nIngrese un número dependiendo de la sucursal de retiro donde desea pasar a buscar su pedido\n------------------------------------");
        Console.WriteLine("[1] DEVOTO \n[2] CABALLITO \n[3] RECOLETA \n[4] LINIERS");
        opcion_elegida = Console.ReadLine();

        if (String.IsNullOrEmpty(opcion_elegida))
        {
            Console.WriteLine("------------------------------------\nERROR - No seleccionó ninguna opcion.");
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
                rsp = "Retiro por Sucursal de DEVOTO";
                break;
            }
        case "2":
            {
                rsp = "Retiro por Sucursal de CABALLITO";
                break;
            }
        case "3":
            {
                rsp = "Retiro por Sucursal de RECOLETA";
                break;
            }
        case "4":
            {
                rsp = "Retiro por Sucursal de LINIERS";
                break;
            }
        default:
            rsp = "Sin Identificar";
            break;
    }
    return rsp;
}


bool consulta_urgencia()
{
    List<string> opciones_validas = new List<string>();
    opciones_validas.Add("1");
    opciones_validas.Add("2");

    string opcion_elegida = "";
    bool bandera = true;
    while (bandera)
    {
        Console.WriteLine("------------------------------------\nPor favor responder correctamente: ¿Es urgente el envio de este pedido? Ingrese el número segén corresponda");
        Console.WriteLine("[1] Si \n[2] No");
        opcion_elegida = Console.ReadLine();

        if (String.IsNullOrEmpty(opcion_elegida))
        {
            Console.WriteLine("------------------------------------\nERROR - No seleccionó ninguna opcion.");
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

    bool rsp = false;
    switch (opcion_elegida)
    {
        case "1":
            {
                rsp = true;
                break;
            }
        case "2":
            {
                rsp = false;
                break;
            }
    }
    return rsp;
}