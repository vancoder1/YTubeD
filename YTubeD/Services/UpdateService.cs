using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Onova;
using Onova.Exceptions;
using Onova.Services;

namespace YTubeD.Services
{
    class UpdateService
    {
        private readonly IUpdateManager? _updateManager = OperatingSystem.IsWindows()
        ? new UpdateManager(
            new GithubPackageResolver(
                "vancoder1",
                "YTubeD",
                $"YTubeD.{RuntimeInformation.RuntimeIdentifier}.zip"
            ),
            new ZipPackageExtractor()
        )
        : null;

        private Version? _updateVersion;
        private bool _updatePrepared;
        private bool _updaterLaunched;

        public async Task<Version?> CheckForUpdatesAsync()
        {
            if (_updateManager is null)
                return null;

            var check = await _updateManager.CheckForUpdatesAsync();
            return check.CanUpdate ? check.LastVersion : null;
        }

        public async Task PrepareUpdateAsync(Version version)
        {
            if (_updateManager is null)
                return;

            try
            {
                await _updateManager.PrepareUpdateAsync(_updateVersion = version);
                _updatePrepared = true;
            }
            catch (UpdaterAlreadyLaunchedException)
            {
                // Ignore race conditions
            }
            catch (LockFileNotAcquiredException)
            {
                // Ignore race conditions
            }
        }

        public void FinalizeUpdate(bool needRestart)
        {
            if (_updateManager is null)
                return;

            if (!OperatingSystem.IsWindows())
                return;

            if (_updateVersion is null || !_updatePrepared || _updaterLaunched)
                return;

            try
            {
                _updateManager.LaunchUpdater(_updateVersion, needRestart);
                _updaterLaunched = true;
            }
            catch (UpdaterAlreadyLaunchedException)
            {
                // Ignore race conditions
            }
            catch (LockFileNotAcquiredException)
            {
                // Ignore race conditions
            }
        }
    }
}
