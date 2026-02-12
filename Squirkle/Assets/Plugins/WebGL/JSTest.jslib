mergeInto(LibraryManager.library, 
{
    OnPlayerGiveCoins: function(coins) 
    {
        window.dispatchReactUnityEvent("OnPlayerGiveCoins", coins);
    }
});
