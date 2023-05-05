class infoController
{
    private List<InfoModel> _info;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    static public AccountModel? CurrentAccount { get; private set; }

    public infoController()
    {
        _info = InfoAccess.LoadAll();
    }

}