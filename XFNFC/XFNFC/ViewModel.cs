using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace XFNFC
{
    public abstract class ViewModel : ReactiveObject
    {

        CompositeDisposable? deactivateWith;
        protected CompositeDisposable DeactivateWith => this.deactivateWith ??= new CompositeDisposable();
        protected CompositeDisposable DestroyWith { get; } = new CompositeDisposable();


        protected virtual void Deactivate()
        {
            this.deactivateWith?.Dispose();
            this.deactivateWith = null;
        }


        public virtual void OnAppearing() { }
        public virtual void OnDisappearing() { }
        public virtual void Destroy() => this.DestroyWith?.Dispose();
    }
}
