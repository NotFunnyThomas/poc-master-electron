using Domain.Mom;
using Domain.Tools;

namespace Domain
{
    public interface IServiceDeamon
    {
        void Initialize();
        void UnInitialize();
        void Run();
        void Stop();
        void Pause();
        void Resume();
        void EmissionMessage(MessageComm msg);

        public event InfoFromListener InfoDlg;
    }
}
