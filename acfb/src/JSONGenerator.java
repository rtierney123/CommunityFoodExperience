package testing;

import java.util.ArrayList;

public class JSONGenerator {
    String s;
    int nested;
    ArrayList<Member> memberList;

    public JSONGenerator(ArrayList<Member> mlist) {
        s = "";
        nested = 0;
        memberList = mlist;
    }

    public void generate() {
        openObject();
        int i= 0;
        for (Member m : memberList) {
            i = 0;
            for (String p : m.order) {
                i++;
                tab();
                // new member object
                if (p.contentEquals("memberID")) {
                    s += quote(m.getProp(p))+ ": {\n";
                    nested++;
                } else {
                    s += quote(p) + ": ";
                    boolean isInt = false;
                    try {
                        Integer.parseInt(m.getProp(p));
                        isInt = true;
                    } catch (Exception e) {

                    }

                    if (isInt || m.getProp(p).contentEquals(("true")) ||
                            m.getProp(p).contentEquals(("false")) ||
                            m.getProp(p).charAt(0) == '\"') {
                        s += m.getProp(p);
                    } else {
                        s += quote(m.getProp(p));
                    }
                    s += ",\n";
                }
            }
            s = s.substring(0, s.length()-2);
            s += "\n";
            closeObject();
//			break;
        }
        s = s.substring(0, s.length()-2);

        // DONE
        s += "\n}";
    }

    private void openObject() {
//		System.out.println(nested);
        s += "{\n";
        nested++;
    }

    private void closeObject() {
        tab();
        s+="},";
        s+="\n";
        nested--;
    }

    private String quote(String s) {
        return "\"" + s + "\"";
    }

    private void tab() {
        for (int j=0; j<nested; j++) {
            s += "\t";
        }
    }

    public String out() {
        return s;
    }

}
