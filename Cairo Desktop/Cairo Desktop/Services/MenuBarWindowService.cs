﻿using CairoDesktop.AppGrabber;
using CairoDesktop.Application.Interfaces;
using CairoDesktop.Common;
using CairoDesktop.Infrastructure.ObjectModel;
using CairoDesktop.Infrastructure.Services;
using ManagedShell.AppBar;

namespace CairoDesktop.Services
{
    public class MenuBarWindowService : AppBarWindowService
    {
        private readonly IAppGrabber _appGrabber;
        private readonly ISettingsUIService _settingsUiService;
        private readonly IApplicationUpdateService _updateService;
        private readonly ICommandService _commandService;

        public MenuBarWindowService(ICairoApplication cairoApplication, ShellManagerService shellManagerService, IWindowManager windowManager, IAppGrabber appGrabber, IApplicationUpdateService updateService, ISettingsUIService settingsUiService, ICommandService commandService, Settings settings) : base(cairoApplication, shellManagerService, windowManager, settings)
        {
            _appGrabber = appGrabber;
            _settingsUiService = settingsUiService;
            _updateService = updateService;
            _commandService = commandService;

            EnableMultiMon = _settings.EnableMenuBarMultiMon;
            EnableService = _settings.EnableMenuBar;
        }

        protected override void HandleSettingChange(string setting)
        {
            switch (setting)
            {
                case "EnableMenuBar":
                    HandleEnableServiceChanged(_settings.EnableMenuBar);
                    break;
                case "EnableMenuBarMultiMon":
                    HandleEnableMultiMonChanged(_settings.EnableMenuBarMultiMon);
                    break;
            }
        }

        protected override void OpenWindow(AppBarScreen screen)
        {
            MenuBar newMenuBar = new MenuBar(_cairoApplication, _shellManager, _windowManager, _appGrabber, _updateService, _settingsUiService, _commandService, _settings, screen, _settings.MenuBarEdge, _settings.EnableMenuBarAutoHide ? AppBarMode.AutoHide : AppBarMode.Normal);
            Windows.Add(newMenuBar);
            newMenuBar.Show();
        }
    }
}
