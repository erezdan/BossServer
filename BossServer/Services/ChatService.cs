using BossServer.Data;
using BossServer.Models;
using MongoDB.Driver;

public class ChatService
{
    private readonly IMongoCollection<ChatContext> _chatContexts;
    private readonly IMongoCollection<ChatMessage> _chatMessages;

    public ChatService(IMongoDbContext context)
    {
        _chatContexts = context.ChatContexts;
        _chatMessages = context.ChatMessages;
    }

    // ChatContext methods

    public async Task<List<ChatContext>> GetChatContextsAsync() =>
        await _chatContexts.Find(_ => true).ToListAsync();

    public async Task<ChatContext> GetChatContextAsync(string id) =>
        await _chatContexts.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateChatContextAsync(ChatContext newChatContext) =>
        await _chatContexts.InsertOneAsync(newChatContext);

    public async Task UpdateChatContextAsync(string id, ChatContext updatedChatContext) =>
        await _chatContexts.ReplaceOneAsync(x => x.Id == id, updatedChatContext);

    public async Task RemoveChatContextAsync(string id) =>
        await _chatContexts.DeleteOneAsync(x => x.Id == id);

    // ChatMessage methods

    public async Task<List<ChatMessage>> GetChatMessagesAsync(string chatContextId) =>
        await _chatMessages.Find(x => x.ChatContextId == chatContextId).ToListAsync();

    public async Task<ChatMessage> GetChatMessageAsync(string id) =>
        await _chatMessages.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateChatMessageAsync(ChatMessage newChatMessage)
    {
        await _chatMessages.InsertOneAsync(newChatMessage);

        // Update ChatContext with new message
        var update = Builders<ChatContext>.Update.Push("MessageIds", newChatMessage.Id);
        await _chatContexts.UpdateOneAsync(c => c.Id == newChatMessage.ChatContextId, update);
    }

    public async Task UpdateChatMessageAsync(string id, ChatMessage updatedChatMessage) =>
        await _chatMessages.ReplaceOneAsync(x => x.Id == id, updatedChatMessage);

    public async Task RemoveChatMessageAsync(string id)
    {
        var message = await _chatMessages.Find(x => x.Id == id).FirstOrDefaultAsync();
        if (message != null)
        {
            await _chatMessages.DeleteOneAsync(x => x.Id == id);

            // Update ChatContext to remove the message ID
            var update = Builders<ChatContext>.Update.Pull("MessageIds", id);
            await _chatContexts.UpdateOneAsync(c => c.Id == message.ChatContextId, update);
        }
    }
}


