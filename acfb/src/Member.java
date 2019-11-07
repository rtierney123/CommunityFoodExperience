package testing;

import java.util.ArrayList;
import java.util.HashMap;

public class Member{

    HashMap<String, String> properties;
    ArrayList<String> order;

    public Member(Member m) {
        properties = m.properties;
        order = m.order;
    }

    public Member(String id) {
        properties = new HashMap<String, String>();
        order = new ArrayList<String>();

        properties.put("memberID", id);

//		#Pantry
        properties.put("lastName", "");
        properties.put("firstName", "");
        properties.put("middleInitial", "");
        properties.put("street", "");
        properties.put("city", "");
        properties.put("state", "");
        properties.put("zip", "");
        properties.put("phone", "");
        properties.put("numChildren", "");
        properties.put("monthlyIncome", "");
        properties.put("childrenAges", "");
        properties.put("fedAssistance", "");
//		#SNAP
        properties.put("gender", "");
        properties.put("dob", "");
        properties.put("ssn", "");
        properties.put("primaryLanguage", "");
        properties.put("numInHousehold", "");
        properties.put("isPregnant", "");
        properties.put("isEmployed", "");
        properties.put("legallyInUS", "");
        properties.put("hourlyWage", "");
        properties.put("transportation", "");
        properties.put("fleeingLaw", "");
        properties.put("paroleViolation", "");
        properties.put("fraud", "");
        properties.put("controlledSubstance", "");
        properties.put("soldFoodstamps", "");
        properties.put("understood", "");
//		#VITA
        properties.put("fullName", "");
        properties.put("single", "");
        properties.put("married", "");
        properties.put("jointTax", "");
        properties.put("over6Months", "");
        properties.put("allIncomeInUS", "");
        properties.put("lessThan3150", "");

        order.add("memberID");
//		#Pantry
        order.add("lastName");
        order.add("firstName");
        order.add("middleInitial");
        order.add("street");
        order.add("city");
        order.add("state");
        order.add("zip");
        order.add("phone");
        order.add("numChildren");
        order.add("monthlyIncome");
        order.add("childrenAges");
        order.add("fedAssistance");
//		#SNAP
        order.add("gender");
        order.add("dob");
        order.add("ssn");
        order.add("primaryLanguage");
        order.add("numInHousehold");
        order.add("isPregnant");
        order.add("isEmployed");
        order.add("legallyInUS");
        order.add("hourlyWage");
        order.add("transportation");
        order.add("fleeingLaw");
        order.add("paroleViolation");
        order.add("fraud");
        order.add("controlledSubstance");
        order.add("soldFoodstamps");
        order.add("understood");
//		#VITA
        order.add("fullName");
        order.add("single");
        order.add("married");
        order.add("jointTax");
        order.add("over6Months");
        order.add("allIncomeInUS");
        order.add("lessThan3150");
    }

    public void overrideProp(String n, String v) {
        properties.put(n, v);
    }

    public void setProp(String n, String v) {
        // prevent overriding
        if (properties.get(n).equals("")) {
            properties.put(n, v);
        }
    }

    public String getProp(String n) {
        return properties.get(n);
    }

    public String toString() {
        String s = "";
        int max = 0;
        for (String k : order) {
            if (k.length() > max) {
                max = k.length();
            }
        }
        for (String k : order) {
            s += k;
            for (int i=0; i<max-k.length(); i++) {
                s += " ";
            }
            s += "\t" + properties.get(k);
            s += "\n";
        }

        return s;
    }
}
