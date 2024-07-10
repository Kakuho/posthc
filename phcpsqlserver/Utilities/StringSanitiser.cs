namespace Phc.Utilities{

  static class StringSanitiser{
      // essentially a class which checks if there is an existing string with that value
    static public string Transform(string val){
      return val.ToLower().TrimEnd().TrimStart();
    }

    static public bool Compare(string s1, string s2){
      return Transform(s1) == Transform(s2);
    }
  };

}
