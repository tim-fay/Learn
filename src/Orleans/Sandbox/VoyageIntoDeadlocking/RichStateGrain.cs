using System;
using System.Threading.Tasks;
using Orleans;

namespace VoyageIntoDeadlocking
{
    public interface IRichData
    {
        string Text { get; }
        DateTime Date { get; }
        int Index { get; }
    }

    class RichData : IRichData
    {
        public string Text { get; }
        public DateTime Date { get; }
        public int Index { get; }

        private RichData(string text, DateTime date, int index)
        {
            Text = text;
            Date = date;
            Index = index;
        }

        public static IRichData New(string text, DateTime date, int index)
        {
            return new RichData(text, date, index);
        }

        public override string ToString() => $"Text: {Text}; Date: {Date}; Index: {Index}";
    }


    public class RichState
    {
        public IRichData Data { get; private set; }

        public void SetRichData(IRichData newData)
        {
            Data = newData;
        }
    }

    public interface IRichStateGrain : IGrainWithStringKey
    {
        Task SaveState(IRichData data);
        Task SaveState(string text, DateTime date, int index);
        Task<IRichData> ReadState();
    }

    public class RichStateGrain : Grain<RichState>, IRichStateGrain
    {
        public override Task OnActivateAsync()
        {
            return base.OnActivateAsync();
        }

        public async Task SaveState(IRichData data)
        {
            State.SetRichData(data);
            await WriteStateAsync();
            Console.WriteLine($"Saving state for: {State.Data}");
        }

        public async Task SaveState(string text, DateTime date, int index)
        {
            var data = RichData.New(text, date, index);
            State.SetRichData(data);
            await WriteStateAsync();
            Console.WriteLine($"Saving state for: {State.Data}");
        }

        public Task<IRichData> ReadState()
        {
            return Task.FromResult(State.Data);
        }
    }
}