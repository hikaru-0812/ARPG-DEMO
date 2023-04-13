using Manager;
using MyUnityFramework;
using XLua;

public class LuaEntrance : SingletonAutoMono<LuaEntrance>
{
    private readonly LuaEnv _env = new LuaEnv();

    private void Start()
    {
        LuaManager.GetInstance().Init();
        LuaManager.GetInstance().DoLuaFile("LuaMain");
    }
}
