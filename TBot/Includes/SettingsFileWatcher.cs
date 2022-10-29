using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace Tbot.Includes {
	public class SettingsFileWatcher {

		private Action _watchFunc;
		private string _absFpToWatch;
		private PhysicalFileProvider p;
		private IChangeToken changeToken;
		private IDisposable changeCallback;

		public SettingsFileWatcher(Action func, string filePathToWatch) {
			_watchFunc = func;
			_absFpToWatch = filePathToWatch;

			initWatch();
		}

		private void initWatch() {
			p = new PhysicalFileProvider(Path.GetDirectoryName(_absFpToWatch));
			changeToken = p.Watch(Path.GetFileName(_absFpToWatch));
			changeCallback = changeToken.RegisterChangeCallback(onChanged, default);
		}

		public void deinitWatch() {
			if (changeCallback != null ) {
				changeCallback.Dispose();
				changeCallback = null;
			}
		}

		private void onChanged(object state) {

			_watchFunc();

			initWatch();
		}
	}
}