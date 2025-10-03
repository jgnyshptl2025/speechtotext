import java.io.*;

class Geeks {
    public static void main (String[] args) {
      
        // Initializing String variable with null value
        String s = null;

        // Checking if s.equals null
        try
        {
            // This line of code throws NullPointerException because s is null
            if (s.equals("gfg"))
                System.out.print("Same");
            else 
                System.out.print("Not Same");
        }
        catch(NullPointerException e)
        {
            System.out.print("NullPointerException Caught");
        }
    }
}
