using ccode.core.ValueObjects;

namespace ccode.core.Services.Interfaces;

public interface IChatBotService
{
    IAsyncEnumerable<Prompt> ProcessPrompt(
        Prompt prompt);
}