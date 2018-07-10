#include <iostream>
#include <string>
#include <vector>
#include <fstream>

using namespace std;

//CLASSES --------------------------------

//Singleton used because is a singleplayer game, thus, just one user can be added.
class User
{
    private:
        static User* unique; //Unique user object
        string nickname;

        User(string nick)
        {
            nickname = nick;
        }
    public:
        static User* getInst(string nick);

        string getNick()
        {
            return nickname;
        }
};

User* User::unique = NULL;

User* User::getInst(string nick)
{
    if(unique == NULL)
        unique = new User(nick);

    return unique;
}

class Somebody
{
    public:
        string name;
        int atk;
        int def;
        int level;
        int life;
};

class Character: public Somebody
{
    public:
        Character(string n)
        {   
            name = n;
            level = 1;
            atk = 10;
            def = 10;
            life = 120;
        }

        void showStats()
        {
            cout << name << endl;
            cout << "LVL: " << level;
            cout << " ATK: " << atk;
            cout << " DEF: " << def;
            cout << " LIFE: " << life << endl << endl;
        }
};

class Monster: public Somebody
{
    public:
        Monster(string n, int lvl)
        {
            name = n;
            level = lvl;
            atk = lvl*8;
            def = lvl*8; 
            life = lvl*10;
        }

        void showMonster()
        {
            cout << "Name: " << name << endl;
            cout << "LVL: " << level << endl;
            cout << "ATK: " << atk << endl;
            cout << "DEF: " << def << endl;
            cout << "LIFE: " << life << endl;  
        }
};

//---------------------------------------

//FUNCTIONS -----------------------------

void menu()
{
    cout << "[-----------------]" << endl;
    cout << "[--- Game Name ---]" << endl;
    cout << "[-----------------]" << endl;
    cout << "[---- > Start ----]" << endl;
    cout << "[-----------------]" << endl;
    getchar();
    system("clear");
}

void showOptions()
{
    cout << "1 - Attack" << endl;
    cout << "2 - Defend" << endl;
    cout << "> ";
}

int save(string username, Character *character, int fase)
{
    ofstream file;

    cout << "Saving..." << endl;
    file.open("pw.save", ofstream::out);

    file << "------SAVE------" << endl;
    file << username << endl;
    file << character->name << endl;
    file << character->level << endl;
    file << character->atk << endl;
    file << character->def << endl;

    file.close();

    return 1;
}

int wannaSave()
{
    int save_op;

    cout << "Save?" << endl;
    cout << "1 - Yes" << endl;
    cout << "2 - No" << endl;
    cout << "> ";
    cin >> save_op;

    return save_op;
}

int tryAgain()
{
    int op;

    cout << "Try again?" << endl;
    cout << "1 - Yes" << endl;
    cout << "2 - No" << endl;
    cout << "> ";

    cin >> op;

    return op;
}

int attack(Somebody *a, Somebody *b)
{
    b->life += -(a->atk - b->def);
}

//FASES

int fase1(Character *character)
{
    int op;
    Monster *monster1 = new Monster("Esqueleto", 1);

    cout << "You found a Monster!" << endl;
    monster1->showMonster();

    cout << "Begin Battle [ PRESS ENTER ]";
    getchar();

    while((monster1->life > 0) && (character->life > 0))
    {
        system("clear");
        character->showStats();
        monster1->showMonster();

        showOptions();
        cin >> op;
        if(op == 1)
        {
            attack(character, monster1);
        }
    }
    if((monster1->life <= 0) && (character->life > 0))
    {
        cout << "Monster defeated!" << endl;
        return 1;
    }
    else
    {
        cout << "You lose!" << endl;
        return 0;
    }
}
//---------------------------------------

int main()
{
    string nickname;
    string charname;
    int fase = 0;
    int op;
    
    Character *character;

    // User *admin = new User("admin");
    menu();

    cout << "Type your nickname: ";
    cin >> nickname;
    User *user = User::getInst(nickname);

    cout << "Welcome " << user->getNick() << "!" << endl;
    
    cout << "New Character [ PRESS ENTER ]";
    getchar();
    getchar();

    cout << "Name: ";
    cin >> charname;
    character = new Character(charname);
    
    cout << "You're gonna be a great warrior " << charname << endl;
    cout << "Begin journey [ PRESS ENTER ]";
    getchar();
    getchar();
    system("clear");
    character->showStats();

    fase = fase1(character);
    while(fase == 0)
    {
        fase = fase1(character);

        if(tryAgain() == 2)
        {
            break;
        }
    }

    if(wannaSave() == 1)
    {
        save(nickname, character, fase);
    }

    return 0;
}
//CONTINUAR FASE 1 e FAZER LOAD