mergeInto(LibraryManager.library, 
{
    SendGameTime: function(time) 
    {
        window.dispatchReactUnityEvent("GameTime", time);
    }
});
