using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shiny;
using Shiny.Nfc;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Windows.Input;

namespace XFNFC
{
    public class MainPageViewModel : ViewModel
    {
        readonly INfcManager nfcManager;


        public MainPageViewModel(INfcManager nfcManager = null)
        {
            this.nfcManager = Shiny.ShinyHost.Resolve<INfcManager>();

            this.CheckPermission = ReactiveCommand.Create(async () =>
            {
                this.Access = await nfcManager.RequestAccess().ToTask();
            });

            this.Clear = ReactiveCommand.Create(() =>
                this.ChangeRecords(() => this.NDefRecords.Clear())
            );

            this.Listen = ReactiveCommand.Create(() =>
            {
                if (this.IsListening)
                {
                    this.IsListening = false;
                    this.Deactivate();
                }
                else
                {
                    this.nfcManager
                        .Reader()
                        .SubOnMainThread(x =>
                            this.ChangeRecords(() =>
                            {
                                this.NDefRecords.AddRange(x);
                            })
                        )
                        .DisposeWith(this.DeactivateWith);
                    this.IsListening = true;
                }
            });

        }


        void ChangeRecords(Action action)
        {
            lock (this.NDefRecords)
                action();

            this.RaisePropertyChanged(nameof(NDefRecords));
        }


        public ICommand Clear { get; }
        public ICommand Listen { get; }
        public ICommand CheckPermission { get; }
        public List<NDefRecord> NDefRecords { get; } = new List<NDefRecord>();
        private AccessState _access = AccessState.Unknown;
        public AccessState Access
        {
            get => _access;
            set
            {
                _access = value;
                this.RaisePropertyChanged(nameof(Access));
            }
        } 
        [Reactive] public bool IsListening { get; private set; }
    }
}
