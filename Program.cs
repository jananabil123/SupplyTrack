using SupplyTrack.Core;
using SupplyTrack.Application.Services;
using SupplyTrack.Core.Repositories;

MaterialRepository materialRepository = new MaterialRepository();
MaterialService materialService = new MaterialService(materialRepository);

SupplyRequestRepository supplyRequestRepository = new SupplyRequestRepository();
SupplyRequestService supplyRequestService = new SupplyRequestService(supplyRequestRepository);
while (true)
{
    ShowMenu();
    Console.Write("Choose an option: ");
    string? choice = Console.ReadLine();

    if (choice == "0")
    {
        break;
    }
    if (choice == "1")
    {
        Material material = new Material();

        Console.Write("Enter Code: ");
        material.Code = Console.ReadLine();

        Console.Write("Enter Name: ");
        material.Name = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(material.Name))
        {
            Console.Write("Name cannot be empty. Enter Name: ");
            material.Name = Console.ReadLine();
        }

        int stock;

        Console.Write("Enter Stock: ");

        while (!int.TryParse(Console.ReadLine(), out stock) || stock < 0)
        {
            Console.Write("Invalid stock. Enter a non-negative number: ");
        }

        material.Stock = stock;
        materialService.AddMaterial(material);
        Console.WriteLine("Material added successfully!");
    }
    if (choice == "2")
    {
        ListMaterials(materialService.GetAllMaterials());
    }
    if (choice == "3")
    {
        Console.Write("Enter material code: ");
        string? code = Console.ReadLine();

        bool found = false;

        foreach (Material material in materialService.GetAllMaterials())
        {
            if (material.Code == code)
            {
                Console.WriteLine($"Code: {material.Code}");
                Console.WriteLine($"Name: {material.Name}");
                Console.WriteLine($"Stock: {material.Stock}");
                found = true;
                break;
            }
        }
        if (!found)
        {
            Console.WriteLine("Material not found.");
        }
    }
    if (choice == "4")
    {
        SupplyRequest request = new SupplyRequest();

        request.Id = supplyRequestService.GetAllRequests().Count + 1;

        Console.Write("Enter requester name: ");
        request.RequesterName = Console.ReadLine();
        Console.Write("How many items? ");
        int itemCount = int.Parse(Console.ReadLine());
        for (int i = 0; i < itemCount; i++)
        {
            RequestLine line = new RequestLine();

            Console.Write("Enter material code: ");
            line.MaterialCode = Console.ReadLine();

            Console.Write("Enter quantity: ");
            line.Quantity = int.Parse(Console.ReadLine());

            request.Lines.Add(line);
        }
        supplyRequestService.AddRequest(request);

        Console.WriteLine("Supply request created successfully!");
    }
    if (choice == "5")
    {
        Console.WriteLine("=== Supply Requests ===");

        foreach (SupplyRequest request in supplyRequestService.GetAllRequests())
        {
            Console.WriteLine($"Request ID: {request.Id}");
            Console.WriteLine($"Requester: {request.RequesterName}");
            Console.WriteLine($"Status: {request.Status}");
            Console.WriteLine();
        }
    }
    if (choice == "6")
    {
        Console.Write("Enter Request ID: ");
        int requestId = int.Parse(Console.ReadLine());
        SupplyRequest foundRequest = null;

        foreach (SupplyRequest request in supplyRequestService.GetAllRequests())
        {
            if (request.Id == requestId)
            {
                foundRequest = request;
                break;
            }
        }
        if (foundRequest != null)
        {
            Console.WriteLine($"Request ID: {foundRequest.Id}");
            Console.WriteLine($"Requester: {foundRequest.RequesterName}");
            Console.WriteLine($"Status: {foundRequest.Status}");
            Console.WriteLine("Items:");

            foreach (RequestLine line in foundRequest.Lines)
            {
                Console.WriteLine($"Material Code: {line.MaterialCode}, Quantity: {line.Quantity}");
            }
        }
        else
        {
            Console.WriteLine("Request not found.");
        }
    }
    if (choice == "7")
    {
        Console.Write("Enter Request ID: ");
        int requestId = int.Parse(Console.ReadLine());

        foreach (SupplyRequest request in supplyRequestService.GetAllRequests())
        {
            if (request.Id == requestId)
            {
                request.Submit();
                break;
            }
        }
    }

    if (choice == "8")
    {
        Console.Write("Enter Request ID: ");
        int requestId = int.Parse(Console.ReadLine());

        foreach (SupplyRequest request in supplyRequestService.GetAllRequests())
        {
            if (request.Id == requestId)
            {
                request.Approve();
                break;
            }
        }
    }

    if (choice == "9")
    {
        Console.Write("Enter Request ID: ");
        int requestId = int.Parse(Console.ReadLine());

        foreach (SupplyRequest request in supplyRequestService.GetAllRequests())
        {
            if (request.Id == requestId)
            {
                request.Reject();
                break;
            }
        }
    }

    if (choice == "10")
    {
        Console.Write("Enter Request ID: ");
        int requestId = int.Parse(Console.ReadLine());

        foreach (SupplyRequest request in supplyRequestService.GetAllRequests())
        {
            if (request.Id == requestId)
            {
                bool enoughStock = true;

                foreach (RequestLine line in request.Lines)
                {
                    foreach (Material material in materialService.GetAllMaterials())
                    {
                        if (material.Code == line.MaterialCode)
                        {
                            if (material.Stock < line.Quantity)
                            {
                                enoughStock = false;
                            }
                        }
                    }
                }

                if (!enoughStock)
                {
                    Console.WriteLine("Not enough stock to fulfill this request.");
                    break;
                }

                foreach (RequestLine line in request.Lines)
                {
                    foreach (Material material in materialService.GetAllMaterials())
                    {
                        if (material.Code == line.MaterialCode)
                        {
                            material.Stock -= line.Quantity;
                        }
                    }
                }

                request.Fulfill();
                break;
            }
        }
    }
    if (choice == "11")
    {
        Console.WriteLine("===== Low Stock Report =====");

        foreach (Material material in materialService.GetAllMaterials())
        {
            if (material.Stock < 20)
            {
                Console.WriteLine($"Code: {material.Code}");
                Console.WriteLine($"Name: {material.Name}");
                Console.WriteLine($"Stock: {material.Stock}");
                Console.WriteLine("----------------------");
            }
        }
    }
}
static void ShowMenu()
{
    Console.WriteLine("1. Add Material");
    Console.WriteLine("2. List Materials");
    Console.WriteLine("3. Search Material");
    Console.WriteLine("4. Create Supply Request");
    Console.WriteLine("5. List Requests");
    Console.WriteLine("6. View Request Details");
    Console.WriteLine("7. Submit Request");
    Console.WriteLine("8. Approve Request");
    Console.WriteLine("9. Reject Request");
    Console.WriteLine("10. Fulfill Request");
    Console.WriteLine("11. Low Stock Report");
    Console.WriteLine("0. Exit");
}
static void ListMaterials(List<Material> materials)
{
    Console.WriteLine("\n===== Materials =====");

    foreach (Material material in materials)
    {
        Console.WriteLine($"Code: {material.Code}");
        Console.WriteLine($"Name: {material.Name}");
        Console.WriteLine($"Stock: {material.Stock}");
        Console.WriteLine("----------------------");
    }
}