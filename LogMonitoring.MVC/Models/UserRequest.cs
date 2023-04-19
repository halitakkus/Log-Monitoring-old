namespace LogMonitoring.MVC.Models;

public class UserRequest
{
    public UserRequest(int? id)
    {
        Id = id;
    }

    public UserRequest()
    {
        
    }
    public  int? Id { get; set; }
}