using ReolinkNVR;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var nvr = new ReolinkNVR.ReolinkNVR(new HttpClient(), "192.168.1.10", "username", "password");
var response = await nvr.SaveStream(DateTime.UtcNow.AddMinutes(-2), DateTime.UtcNow.AddMinutes(-1), 1);
System.Console.WriteLine($"{response.value.fileList[0].fileName}");

await nvr.DownloadVOD(response.value.fileList[0].fileName, "./test-output.mp4");