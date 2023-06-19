using System.Text.RegularExpressions;
using Bancocdis;

if (args.Length==0)
    emailservice.sendMail();
else
   ShowMenu();


void ShowMenu()
{
    Console.Clear();
    Console.WriteLine("Selecciona una opción:");
    Console.WriteLine("1. Crea un usuario nuevo");
    Console.WriteLine("2. Elimina un Usuario Existente");
    Console.WriteLine("3. Salir");

    int option=0;
    do
    {
        string input = Console.ReadLine();

        if(!int.TryParse(input, out option)|| option<=0|| option>3)
            Console.WriteLine("ingresa una opcion válida (1, 2 o 3).");
        else if(option>3)
            Console.WriteLine("ingresa una opcion válida (1, 2 o 3).");
    }while(option ==0 || option >3);

    switch(option)
    {
        case 1:
        CreateUser();break;
        case 2:
        DeleteUser();break;
        case 3:
        Environment.Exit(0);
        break;
    }
}

void CreateUser()
{
    Console.Clear();
    Console.WriteLine("Ingresa la informacion del usuario:");

    int ID;
    do
    {
        Console.Write("ID: ");
        string idInput= Console.ReadLine();

        if(!int.TryParse(idInput, out ID)|| ID <=0)
            Console.WriteLine("El ID debe ser un entero positivo, ingresa otro valor.");
        else if(storage.UserExists(ID))
            Console.WriteLine("El ID ya está asignado a otro usuario, ingresa otro valor.");
    } while (ID<=0 || storage.UserExists(ID));

    
    Console.Write("Nombre: ");
    string name= Console.ReadLine();

    string email;
    do
    {
         Console.Write("Email: ");
         email= Console.ReadLine();

        if(!IsValidEmail(email))
            Console.WriteLine("El formato del correo electronico no es válido");
    } while (!IsValidEmail(email));
   
    decimal balance;
    do
    {
         Console.Write("Saldo: ");
        string balanceInput= Console.ReadLine();

        if (!decimal.TryParse(balanceInput, out balance) || balance <=0)
            Console.WriteLine("El saldo debe ser un decimal positivo, ingresa otro valor");

    }while (balance <=0);
   
    char userType= '\0';
    do
    {
        Console.Write("Escribe 'c' si el usuario es cliente, 'e' si es empleado: ");
        string userTypeInput = Console.ReadLine();

        if (userTypeInput.Length != 1 || (userTypeInput[0] != 'c' && userTypeInput[0] != 'e'))
            Console.WriteLine("Ingresa un valor válido.");
        else
            userType = userTypeInput[0];
    } while (userType != 'c' && userType != 'e');

    User newUser;

    if(userType.Equals('c'))
    {
        Console.Write("Regimen Fiscal: ");
        char TaxRegime= char.Parse(Console.ReadLine());

        newUser = new client(ID, name, email, balance, TaxRegime);
    }
    else
    {
        Console.Write("Departamento: ");
        string Department = Console.ReadLine();

           newUser = new employee(ID, name, email, balance, Department);
    }

    storage.AddUser(newUser);

    Console.WriteLine("Usuario creado.");
    Thread.Sleep(2000);
    ShowMenu();
}

bool IsValidEmail(string email)
{
    string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

    bool isValid = Regex.IsMatch(email, pattern);

    return isValid;
}

void DeleteUser()
{
    Console.Clear();
    int ID;
    bool isValidID = false;

    do
    {
         Console.Write("Ingresa el ID del usuario a eliminar: ");
         string idInput=Console.ReadLine();

         if (!int.TryParse(idInput, out ID) || ID <=0)
         {
            Console.WriteLine("El ID debe ser un entero positivo, ingresa otro valor.");
            continue;
         }

         if(!storage.UserExists(ID))
         {
            Console.WriteLine("El ID no existe, ingresa otro valor");
            continue;
         }
         isValidID= true;
    }while (!isValidID);

    string result = storage.DeleteUser(ID);

    if(result.Equals("Success"))
    {
        Console.Write("Usuario eliminado.");
        Thread.Sleep(2000);
        ShowMenu();
    }

}
