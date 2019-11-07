package testing;

import java.util.ArrayList;

public class Main {

    static String csvPath = "C:\\Users\\Richard\\Downloads\\acfb members.csv";


    public static void main(String[] args) {
        MemberGenerator gen = new MemberGenerator(csvPath);

        gen.readFile();
        gen.randomGeneration();
        ArrayList<Member> memberList = gen.getMembers();

        JSONGenerator jgen = new JSONGenerator(memberList);
        jgen.generate();

        for (Member m : memberList) {
//			System.out.println(m.lastName);
//			System.out.println(m);

        }

        System.out.println(jgen.out());
    }

}
