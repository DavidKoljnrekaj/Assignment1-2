using System.Text;
using System.Text.Json;
using BlazorWasm.Services;
using Shared.Dtos;
using Shared.Model;

namespace BlazorWASM.Services.Http;

public class PostService:IPostService
{
    private readonly HttpClient client;

    public PostService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<ICollection<Post>> getAsync(string? title,string? username)
    {
        string query = "";
        if (!string.IsNullOrEmpty(title))
        {
            query += $"?titleContains={title}";
        }
        else if (!string.IsNullOrEmpty(username))
        {
            query += $"?userName={username}";
        }
        HttpResponseMessage response = await client.GetAsync("/posts" + query);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Post> posts = JsonSerializer.Deserialize<ICollection<Post>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return posts;
    }

    public async Task<Post> getById(int id)
    {
        HttpResponseMessage response = await client.GetAsync("/posts/" + id);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
        Post post = JsonSerializer.Deserialize<Post>(content, new JsonSerializerOptions{
            PropertyNameCaseInsensitive = true
        })!;
        return post;
    }

    public async Task<Post> postAsync(PostCreationDTO dto)
    {
        string userAsJson = JsonSerializer.Serialize(dto);
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync("https://localhost:7164/posts", content);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
        Post post = JsonSerializer.Deserialize<Post>(responseContent, new JsonSerializerOptions{
            PropertyNameCaseInsensitive = true
        })!;
        return post;
        
    }
}