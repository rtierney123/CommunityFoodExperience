package testing;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;

public class MemberGenerator {

    String[] nameList = ("Richard Rogers\r\n" +
            "Juan Powell\r\n" +
            "Johnny Anderson\r\n" +
            "Dennis Lopez\r\n" +
            "Russell Lee\r\n" +
            "James Russell\r\n" +
            "Roy Washington\r\n" +
            "Justin Scott\r\n" +
            "Howard Morris\r\n" +
            "Lawrence Barnes\r\n" +
            "Scott Carter\r\n" +
            "Johnny Ramirez\r\n" +
            "Nicholas Martin\r\n" +
            "Eugene Jackson\r\n" +
            "Howard Young\r\n" +
            "Steven Baker\r\n" +
            "Sean Jenkins\r\n" +
            "Ronald Brown\r\n" +
            "Carlos Powell\r\n" +
            "Joe Martinez\r\n" +
            "Mary Murphy\r\n" +
            "Teresa Henderson\r\n" +
            "Janice Coleman\r\n" +
            "Lisa Mitchell\r\n" +
            "Elizabeth Alexander\r\n" +
            "Diana Bryant\r\n" +
            "Irene Russell\r\n" +
            "Ruby Nelson\r\n" +
            "Jane Jenkins\r\n" +
            "Christine Gray\r\n" +
            "Jennifer Phillips\r\n" +
            "Janice Thomas\r\n" +
            "Evelyn Alexander\r\n" +
            "Lillian Perry\r\n" +
            "Joyce Carter\r\n" +
            "Betty James\r\n" +
            "Michelle Williams\r\n" +
            "Jessica Jenkins\r\n" +
            "Joan Rivera\r\n" +
            "Judy Collins").split("\r\n");
    String[] langList = {"English", "Spanish", "French"};

    ArrayList<Member> memberList;
    Member cm; // current member
    String csvPath;

    public MemberGenerator(String path) {
        csvPath = path;
        System.out.println("generate from: " + csvPath + "\n");

        memberList = new ArrayList<Member>();
    }

    public void readFile() {
        // Read in givens
        BufferedReader reader;
        try {
            reader = new BufferedReader(new FileReader(csvPath));
            String line = reader.readLine();
            while (line != null) {
                parseLine(line);
                line = reader.readLine();
            }
            reader.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void parseLine(String line) {
        line = line.substring(0, line.length() - 2);
//		System.out.println(line);
        String[] split = line.split(",");

        if (split.length > 1) {
//			System.out.println(line);
            if (split[1].charAt(0) == '\"') {
                // list
                String s = "";
                for (int i=1; i<split.length-1; i++) {
                    s += split[i] + ", ";
                }
                s += split[split.length-1];
                cm.overrideProp(split[0], s);
                return;
            }

            // new member
            if (split[1].equals("NEW")) {
                cm = new Member(split[0]);
            }

            // givens
            else if (!split[1].equals("-")) {
                cm.overrideProp(split[0], split[1]);
            }
        } else {
            if (split[0].equals("END")) {
                randomGeneration();
                memberList.add(new Member(cm));
            }
        }
    }

    public void randomGeneration() {
        generateNames();
        generateDate();
        generateRelationships();
        generateLaw();
        generateFinance();

        cm.setProp("fedAssistance", rbs());
        cm.overrideProp("understood", "true");
        cm.setProp("ssn", "XX-XXX-XXXX");
        cm.setProp("transportation", "none");

        int lang = 0;
        int r = ri(1,100);
        if (r<75) {
            lang = 0;
        } else if (r < 90) {
            lang = 1;
        } else {
            lang = 2;
        }
        cm.setProp("primaryLanguage", langList[lang]);
    }

    private void generateFinance() {
        if (cm.getProp("isEmployed").equals("true")) {
            cm.setProp("hourlyWage", "" + ri(9,20));
        } else {

            cm.setProp("hourlyWage", "0");
        }

        cm.setProp("over6Months", "false");
        if (bc(.7)) {
            cm.setProp("over6Months", "true");
        }
        cm.setProp("allIncomeInUS", "false");
        if (bc(.7)) {
            cm.setProp("allIncomeInUS", "true");
        }
        cm.setProp("lessThan3150", "false");
        if (bc(.7)) {
            cm.setProp("lessThan3150", "true");
        }
    }

    private void generateRelationships() {
        if (cm.getProp("gender").equals("female")) {
            if (bc(.1)) {
                cm.setProp("isPregnant", "true");
            } else {
                cm.setProp("isPregnant", "false");
            }
        } else {
            cm.setProp("isPregnant", "false");
        }

        if (bc(.7)) {
            cm.setProp("isEmployed", "true");
        } else {
            cm.setProp("isEmployed", "false");
        }

        if (bc(.6)) {
            cm.setProp("single", "true");
            cm.setProp("married", "false");
            cm.setProp("jointTax", "false");
        } else {
            cm.setProp("single", "false");
            if (bc(.7)) {
                cm.setProp("married", "true");
                if (bc(.8)) {
                    cm.setProp("jointTax", "true");
                } else {
                    cm.setProp("jointTax", "false");
                }
            } else {
                cm.setProp("married", "false");
                cm.setProp("jointTax", "false");
            }
        }
    }

    private void generateLaw() {
        if (bc(.9)) {
            cm.setProp("legallyInUS", "false");
        } else {
            cm.setProp("legallyInUS", "false");
        }
        if (bc(.1)) {
            cm.setProp("fleeingLaw", "true");
        } else {
            cm.setProp("fleeingLaw", "false");
        }
        if (bc(.1)) {
            cm.setProp("paroleViolation", "true");
        } else {
            cm.setProp("paroleViolation", "false");
        }
        if (bc(.1)) {
            cm.setProp("fraud", "true");
        } else {
            cm.setProp("fraud", "false");
        }
        if (bc(.1)) {
            cm.setProp("controlledSubstance", "true");
        } else {
            cm.setProp("controlledSubstance", "false");
        }
        if (bc(.1)) {
            cm.setProp("soldFoodstamps", "true");
        } else {
            cm.setProp("soldFoodstamps", "false");
        }
    }

    private void generateDate() {
        int day = ri(1,28);
        int month = ri(1,12);
        int year = ri(1950, 1997);
        String s = "";
        if (month < 10) {
            s += "0";
        }
        s += month + "/";
        if (day < 10) {
            s += "0";
        }
        s += day + "/";
        s += year;
        cm.setProp("dob", s);
    }

    private void generateNames() {
        int firstIndex;
        if (rb()) {
            cm.setProp("gender", "male");
            firstIndex = ri(0,19);
        } else {
            cm.setProp("gender", "female");
            firstIndex = ri(20,39);
        }
        String firstName = nameList[firstIndex].split(" ")[0];
        String lastName = nameList[ri(0,39)].split(" ")[1];
        String middleInitial = (char)ri(65,90) + ".";
        cm.setProp("firstName", firstName);
        cm.setProp("lastName", lastName);
        cm.setProp("middleInitial", middleInitial);
        cm.setProp("fullName", firstName + " " + middleInitial + " " + lastName);
    }


    public ArrayList<Member> getMembers() {
        return memberList;
    }

    // helpers
    private boolean bc(double d) {
        return Math.random() < d;
    }
    private boolean rb() {
        return Math.random() < 0.5;
    }
    private String rbs() {
        if (rb()) {
            return "true";
        }
        return "false";
    }
    // inclusive
    private int ri(int x, int y) {
        return (int) Math.floor(Math.random()*(y-x+1)) + x;
    }
}
