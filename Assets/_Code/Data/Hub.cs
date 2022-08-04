using Data.Analytics;
using Data.Levels;
using Data.RateUs;
using FluffyUnderware.Curvy;
using SignalsFramework;
using UnityEngine;

namespace Data
{
    /// <summary>
    /// Main events hub that are available to the application for subscription.
    /// Add / Remove them here when necessary
    /// </summary>
    public static class Hub
    {
        #region [Game Flow]

        /// <summary>
        /// Fired when cleanup on systems should be performed.
        /// Can be on level start, or on lobby transition, see what fits best.
        /// </summary>
        public static readonly Signal Cleanup = new Signal();

        /// <summary>
        /// Fired when user has started new game. E.g. when pressed on play button.
        /// Subscribe to this event to initialize / spawn level, start game logic etc.
        /// </summary>
        public static readonly Signal GameStarted = new Signal();

        /// <summary>
        /// Fired when level progress changes
        /// </summary>
        public static readonly Signal<float> LevelProgressChanged = new Signal<float>();

        /// <summary>
        /// Fired when level has been completed
        /// </summary>
        public static readonly Signal LevelComplete = new Signal();

        /// <summary>
        /// Fired when level failed condition met
        /// </summary>
        public static readonly Signal LevelFailed = new Signal();

        /// <summary>
        /// Fired when next level should be loaded
        /// </summary>
        /// <remarks>Loads PlayerProfile.CurrentLevel, make sure to increment</remarks>
        public static readonly Signal LoadLevel = new Signal();

        public static readonly Signal<int> LevelSceneLoaded = new Signal<int>();

        public static readonly Signal<LevelMetaData> LevelDataLoaded = new Signal<LevelMetaData>();

        public static readonly Signal<Transform> ShowObstacleIndicator = new Signal<Transform>();

        public static readonly Signal ObstaclePassed = new Signal();

        public static readonly Signal<Rails> RailsStartReached = new Signal<Rails>();

        public static readonly Signal RailsEndReached = new Signal();
        #endregion

        #region [UI Flow]

        /// <summary>
        /// Fired when Lobby Screen should be displayed
        /// </summary>
        public static readonly Signal RequestLobbyTransition = new Signal();

        /// <summary>
        /// Fired when coin fx completes
        /// </summary>
        public static readonly Signal CoinFlightComplete = new Signal();

        #endregion

        #region [Profile]

        /// <summary>
        /// Fired when soft currency value changes
        /// </summary>
        public static readonly Signal<long> SoftCurrencyChanged = new Signal<long>();

        /// <summary>
        /// Fired when application vibration state changes
        /// </summary>
        public static readonly Signal<bool> VibroDisabled = new Signal<bool>();

        /// <summary>
        /// Fired when application sound state changes
        /// </summary>
        public static readonly Signal<bool> SoundDisabled = new Signal<bool>();

        #endregion

        #region [Rate us]

        /// <summary>
        /// Fired when Rate Us overlay should be shown
        /// </summary>
        public static readonly Signal<RateUsRequest> ShowRateUs = new Signal<RateUsRequest>();

        #endregion

        #region [Purchasing]

        /// <summary>
        /// Fired when purchase restore is requested
        /// </summary>
        public static readonly Signal RestorePurchases = new Signal();

        #endregion

        #region [Analytics]

        /// <summary>
        /// Fired when Rate Us event should be tracked
        /// </summary>
        public static readonly Signal<RateUsTrackData> TrackRateUs = new Signal<RateUsTrackData>();

        #endregion

        #region [Cheats]

        /// <summary>
        /// Fired when soft currency value should be force updated ignoring any logic
        /// </summary>
        public static readonly Signal ForceRefreshSoftCurrency = new Signal();

        #endregion
    }
}