var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;

    response.ContentType = "text/html; charset=utf-8";

    if (request.Path == "/index" && request.Method == "POST")
    {
        IFormFileCollection files = request.Form.Files;
        var uploadPath = $"{Directory.GetCurrentDirectory()}/images";

        Directory.CreateDirectory(uploadPath);

        foreach (var file in files)
        {
            string fullPath = $"{uploadPath}/{file.FileName}";


            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
        await response.WriteAsync("Файлы успешно загружены");
    }
    else
    {
        await response.SendFileAsync("html/index.html");
    }
});

app.Run();
