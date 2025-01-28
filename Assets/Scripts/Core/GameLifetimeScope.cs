using System.Collections.Generic;
using Cube;
using UniTaskPubSub;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private ShakeAnimationController _shakeAnimationController;
    [SerializeField] private GameObject _flyEffectPrefab;
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private BackgroundsLoader _backgroundsLoader;
    [SerializeField] private List<Booster> _boosters;
    [SerializeField] private List<Background> _backgrounds;
    [SerializeField] private GameController _gameController;
    [SerializeField] private List<Skin> _skins;
    [SerializeField] private ShopController _shopController;
    [SerializeField] private LevelsLoader _levelsLoader;
    [SerializeField] private UIController _uiController;
    [SerializeField] private ManagerAvailableLevels _managerAvailableLevels;
    [SerializeField] private ScreenInteractionManager _screenInteractionManager;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_shakeAnimationController);
        builder.RegisterInstance(_flyEffectPrefab);
        builder.RegisterInstance(_levelConfig);
        builder.RegisterInstance(_boosters);
        builder.RegisterInstance(_backgrounds);
        builder.RegisterInstance(_backgroundsLoader);
        builder.RegisterInstance(_gameController);
        builder.RegisterInstance(_skins);
        builder.RegisterInstance(_shopController);
        builder.RegisterInstance(_levelsLoader).AsSelf().AsImplementedInterfaces();
        builder.RegisterInstance(_uiController);
        builder.RegisterInstance(_managerAvailableLevels);
        builder.RegisterInstance(_screenInteractionManager);

        builder.Register<AsyncMessageBus>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        builder.Register<ObstacleDetector>(Lifetime.Singleton);
        builder.Register<EffectFactory>(Lifetime.Singleton);
        builder.Register<LevelResourceCounter>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        builder.Register<Inventory>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        builder.Register<Wallet>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        builder.Register<MovesCounter>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        builder.Register<LevelTimer>(Lifetime.Singleton);
        builder.Register<ScoreController>(Lifetime.Singleton);
        builder.Register<GameSaveManager>(Lifetime.Singleton);
        builder.Register<ScenesLoader>(Lifetime.Singleton);
    }
}