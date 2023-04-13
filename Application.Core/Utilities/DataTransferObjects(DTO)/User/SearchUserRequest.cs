namespace Application.Core.Utilities.DataTransferObjects.User;

public class SearchUserRequest
{
    public SearchUserRequest(string searchText)
    {
        Icerik = searchText;
    }

    public SearchUserRequest()
    {
        
    }
    
    public string Icerik { get; set; }
}