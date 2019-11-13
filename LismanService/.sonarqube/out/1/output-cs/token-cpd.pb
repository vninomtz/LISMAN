∏D
òC:\Users\Vik-t\Documents\Software Engineering\5to Semestre\Tecnolog√≠as para la Construcci√≥n\Proyecto\LISMAN\LismanService\LismanService\ChatManager.cs
	namespace 	
LismanService
 
{ 
public		 

partial		 
class		 
LismanService		 &
:		' (
IChatManager		) 5
{		6 7
static

 

Dictionary

 
<

 
String

  
,

  ! 
IChatManagerCallBack

" 6
>

6 7!
connectionChatService

8 M
=

N O
new

P S

Dictionary

T ^
<

^ _
String

_ e
,

e f 
IChatManagerCallBack

g {
>

{ |
(

| }
)

} ~
;

~ 
public 
void 
JoinChat 
( 
string #
user$ (
,( )
int* -
idgame. 4
)4 5
{ 	
var 

connection 
= 
OperationContext -
.- .
Current. 5
.5 6
GetCallbackChannel6 H
<H I 
IChatManagerCallBackI ]
>] ^
(^ _
)_ `
;` a
if 
( !
connectionChatService %
.% &
ContainsKey& 1
(1 2
user2 6
)6 7
)7 8
{9 :!
connectionChatService %
[% &
user& *
]* +
=, -

connection. 8
;8 9
} 
else 
{ !
connectionChatService %
.% &
Add& )
() *
user* .
,. /

connection0 :
): ;
;; <
} 
foreach 
( 
var 
userGame !
in" $
listGamesOnline% 4
[4 5
idgame5 ;
]; <
)< =
{> ?!
connectionChatService %
[% &
userGame& .
]. /
./ 0
NotifyNumberPlayers0 C
(C D
listGamesOnlineD S
[S T
idgameT Z
]Z [
.[ \
Count\ a
)a b
;b c
if 
( 
userGame 
!= 
user  $
)$ %
{& '!
connectionChatService )
[) *
userGame* 2
]2 3
.3 4
NotifyJoinedPlayer4 F
(F G
userG K
)K L
;L M
} 
} 
}   	
public"" 
void"" 
	LeaveChat"" 
("" 
string"" $
user""% )
,"") *
int""+ .
idgame""/ 5
)""5 6
{""7 8
try## 
{## 
foreach$$ 
($$ 
var$$ 
userGame$$ %
in$$& (
listGamesOnline$$) 8
[$$8 9
idgame$$9 ?
]$$? @
)$$@ A
{$$B C!
connectionChatService%% )
[%%) *
userGame%%* 2
]%%2 3
.%%3 4
NotifyNumberPlayers%%4 G
(%%G H
listGamesOnline%%H W
[%%W X
idgame%%X ^
]%%^ _
.%%_ `
Count%%` e
)%%e f
;%%f g
}&& 
foreach'' 
('' 
var'' 
userGame'' %
in''& (
listGamesOnline'') 8
[''8 9
idgame''9 ?
]''? @
)''@ A
{''B C!
connectionChatService(( )
[(() *
userGame((* 2
]((2 3
.((3 4
NotifyLeftPlayer((4 D
(((D E
user((E I
)((I J
;((J K
}++ 
},, 
catch,, 
(,,  
KeyNotFoundException,, )
ex,,* ,
),,, -
{,,. /
Logger-- 
.-- 
log-- 
.-- 
Error--  
(--  !
$str--! 7
+--8 9
ex--: <
)--< =
;--= >
}.. 
}00 	
public33 
void33 
SendMessage33 
(33  
Message33  '
message33( /
,33/ 0
int331 4
idgame335 ;
)33; <
{44 	
foreach55 
(55 
var55 
userGame55 !
in55" $
listGamesOnline55% 4
[554 5
idgame555 ;
]55; <
)55< =
{55> ?!
connectionChatService66 )
[66) *
userGame66* 2
]662 3
.663 4
NotifyMessage664 A
(66A B
message66B I
)66I J
;66J K
}77 
}88 	
public:: 
void:: 
	StartGame:: 
(:: 
string:: $
user::% )
,::) *
int::+ .
idgame::/ 5
)::5 6
{;; 	
if<< 
(<< 
!<< &
multiplayerGameInformation<< +
.<<+ ,
ContainsKey<<, 7
(<<7 8
idgame<<8 >
)<<> ?
)<<? @
{== 
Game>> 
informationGame>> $
=>>% &
new>>' *
Game>>+ /
{?? 
gameMap@@ 
=@@ 
GAMEMAP@@ %
,@@% &
lismanUsersAA 
=AA  !
newAA" %

DictionaryAA& 0
<AA0 1
stringAA1 7
,AA7 8
InformationPlayerAA9 J
>AAJ K
(AAK L
)AAL M
}CC 
;CC &
multiplayerGameInformationDD *
.DD* +
AddDD+ .
(DD. /
idgameDD/ 5
,DD5 6
informationGameDD7 F
)DDF G
;DDG H
}EE 
foreachFF 
(FF 
varFF 
userGameFF !
inFF" $
listGamesOnlineFF% 4
[FF4 5
idgameFF5 ;
]FF; <
)FF< =
{FF> ?
ifHH 
(HH 
AssignColorPlayerHH %
(HH% &
idgameHH& ,
,HH, -
userGameHH. 6
)HH6 7
)HH7 8
{II !
connectionChatServiceJJ )
[JJ) *
userGameJJ* 2
]JJ2 3
.JJ3 4
InitGameJJ4 <
(JJ< =
)JJ= >
;JJ> ?
}KK 
}MM 
}NN 	
privatePP 
boolPP 
AssignColorPlayerPP &
(PP& '
intPP' *
idgamePP+ 1
,PP1 2
StringPP3 9
userPP: >
)PP> ?
{QQ 	
InformationPlayerRR 

infoPlayerRR (
=RR) *
newRR+ .
InformationPlayerRR/ @
(RR@ A
)RRA B
;RRB C
intSS 
indexSS 
=SS 
listGamesOnlineSS '
[SS' (
idgameSS( .
]SS. /
.SS/ 0
	FindIndexSS0 9
(SS9 :
uSS: ;
=>SS< >
uSS? @
==SSA C
userSSD H
)SSH I
;SSI J
boolTT 
resultTT 
=TT 
falseTT 
;TT  
switchUU 
(UU 
indexUU 
)UU 
{VV 
caseWW 
$numWW 
:WW 

infoPlayerXX 
.XX 
colorLismanXX *
=XX+ ,
LISMANYELLOWXX- 9
;XX9 :
breakYY 
;YY 
caseZZ 
$numZZ 
:ZZ 

infoPlayer[[ 
.[[ 
colorLisman[[ *
=[[+ ,

LISMANBLUE[[- 7
;[[7 8
break\\ 
;\\ 
case]] 
$num]] 
:]] 

infoPlayer^^ 
.^^ 
colorLisman^^ *
=^^+ ,
	LISMANRED^^- 6
;^^6 7
break__ 
;__ 
case`` 
$num`` 
:`` 

infoPlayeraa 
.aa 
colorLismanaa *
=aa+ ,
LISMANGREENaa- 8
;aa8 9
breakbb 
;bb 
}cc &
multiplayerGameInformationdd &
[dd& '
idgamedd' -
]dd- .
.dd. /
lismanUsersdd/ :
.dd: ;
Adddd; >
(dd> ?
userdd? C
,ddC D

infoPlayerddE O
)ddO P
;ddP Q
ifff 
(ff &
multiplayerGameInformationff *
[ff* +
idgameff+ 1
]ff1 2
.ff2 3
lismanUsersff3 >
[ff> ?
userff? C
]ffC D
.ffD E
colorLismanffE P
!=ffQ S
$numffT U
)ffU V
{gg 
resulthh 
=hh 
truehh 
;hh 
}ii 
returnkk 
resultkk 
;kk 
}ll 	
}mm 
}nn ±
òC:\Users\Vik-t\Documents\Software Engineering\5to Semestre\Tecnolog√≠as para la Construcci√≥n\Proyecto\LISMAN\LismanService\LismanService\GameManager.cs
	namespace 	
LismanService
 
{ 
public 

partial 
class 
LismanService &
:' (
IGameManager) 5
{6 7
static 

Dictionary 
< 
int 
, 
List "
<" #
String# )
>) *
>* +
listGamesOnline, ;
=< =
new> A

DictionaryB L
<L M
intM P
,P Q
ListQ U
<U V
StringV \
>\ ]
>] ^
(^ _
)_ `
;` a
public 
int 

CreateGame 
( 
string $
user% )
)) *
{ 	
Random 
random 
= 
new 
Random  &
(& '
)' (
;( )
int 
idgame 
= 
random 
.  
Next  $
($ %
$num% (
)( )
;) *
var 

listPlayer 
= 
new  
List! %
<% &
String& ,
>, -
(- .
). /
;/ 0
listGamesOnline 
. 
Add 
(  
idgame  &
,& '

listPlayer( 2
)2 3
;3 4
listGamesOnline 
[ 
idgame "
]" #
.# $
Add$ '
(' (
user( ,
), -
;- .
return 
idgame 
; 
} 	
public 
int 
JoinGame 
( 
string "
user# '
)' (
{ 	
foreach 
( 
KeyValuePair !
<! "
int" %
,% &
List' +
<+ ,
String, 2
>2 3
>3 4
games5 :
in; =
listGamesOnline> M
)M N
{O P
if 
( 
games 
. 
Value 
. 
Count $
<% &
$num' (
)( )
{* +
games   
.   
Value   
.    
Add    #
(  # $
user  $ (
)  ( )
;  ) *
return!! 
games!!  
.!!  !
Key!!! $
;!!$ %
}"" 
}## 
return%% 
-%% 
$num%% 
;%% 
}&& 	
public(( 
int(( 
	LeaveGame(( 
((( 
string(( #
user(($ (
,((( )
int((* -
game((. 2
)((2 3
{)) 	
int** 
isDelete** 
=** 
$num** 
;** 
var++ 
listGameUserNames++ !
=++" #
listGamesOnline++$ 3
[++3 4
game++4 8
]++8 9
;++9 :
listGameUserNames,, !
.,,! "
Remove,," (
(,,( )
user,,) -
),,- .
;,,. /
if-- 
(-- 
listGameUserNames-- %
.--% &
Count--& +
==--, .
$num--/ 0
)--0 1
{--2 3
listGamesOnline.. #
...# $
Remove..$ *
(..* +
game..+ /
)../ 0
;..0 1
}// 
return00 
isDelete00 
;00 
}11 	
}22 
}33 æ"
ôC:\Users\Vik-t\Documents\Software Engineering\5to Semestre\Tecnolog√≠as para la Construcci√≥n\Proyecto\LISMAN\LismanService\LismanService\IChatManager.cs
	namespace		 	
LismanService		
 
{		 
[

 
ServiceContract

 
(

 
CallbackContract

 %
=

& '
typeof

( .
(

. / 
IChatManagerCallBack

/ C
)

C D
)

D E
]

E F
public 

	interface 
IChatManager !
{" #
[ 	
OperationContract	 
( 
IsOneWay #
=$ %
true& *
)* +
]+ ,
void 
SendMessage 
( 
Message  
message! (
,( )
int* -
Game. 2
)2 3
;3 4
[ 	
OperationContract	 
( 
IsOneWay #
=$ %
true& *
)* +
]+ ,
void 
JoinChat 
( 
String 
user !
,! "
int# &
idgame' -
)- .
;. /
[ 	
OperationContract	 
( 
IsOneWay #
=$ %
true& *
)* +
]+ ,
void 
	LeaveChat 
( 
String 
user "
," #
int$ '
idgame( .
). /
;/ 0
[ 	
OperationContract	 
( 
IsOneWay #
=$ %
true& *
)* +
]+ ,
void 
	StartGame 
( 
String 
user "
," #
int$ '
idgame( .
). /
;/ 0
} 
[ 
ServiceContract 
] 
public 

	interface  
IChatManagerCallBack )
{* +
[ 	
OperationContract	 
( 
IsOneWay #
=$ %
true& *
)* +
]+ ,
void 
NotifyMessage 
( 
Message "
message# *
)* +
;+ ,
[ 	
OperationContract	 
( 
IsOneWay #
=$ %
true& *
)* +
]+ ,
void 
NotifyJoinedPlayer 
(  
String  &
user' +
)+ ,
;, -
[ 	
OperationContract	 
( 
IsOneWay #
=$ %
true& *
)* +
]+ ,
void 
NotifyNumberPlayers  
(  !
int! $
numberPlayers% 2
)2 3
;3 4
[ 	
OperationContract	 
( 
IsOneWay #
=$ %
true& *
)* +
]+ ,
void 
NotifyLeftPlayer 
( 
String $
user% )
)) *
;* +
[   	
OperationContract  	 
(   
IsOneWay   #
=  $ %
true  & *
)  * +
]  + ,
void!! 
InitGame!! 
(!! 
)!! 
;!! 
}"" 
[$$ 
DataContract$$ 
]$$ 
public%% 

partial%% 
class%% 
Message%%  
{%%! "
[&& 	

DataMember&&	 
]&& 
public'' 
int'' 
Id'' 
{'' 
get'' 
;'' 
set''  
;''  !
}''" #
[(( 	

DataMember((	 
](( 
public)) 
string)) 
Text)) 
{)) 
get))  
;))  !
set))" %
;))% &
}))' (
[** 	

DataMember**	 
]** 
public++ 
System++ 
.++ 
DateTime++ 
Creation_date++ ,
{++- .
get++/ 2
;++2 3
set++4 7
;++7 8
}++9 :
[-- 	

DataMember--	 
]-- 
public.. 
virtual.. 
Account.. 
Account.. &
{..' (
get..) ,
;.., -
set... 1
;..1 2
}..3 4
}// 
}00 ‡
ôC:\Users\Vik-t\Documents\Software Engineering\5to Semestre\Tecnolog√≠as para la Construcci√≥n\Proyecto\LISMAN\LismanService\LismanService\IGameManager.cs
	namespace		 	
LismanService		
 
{		 
[ 
ServiceContract 
] 
public 

	interface 
IGameManager !
{" #
[ 	
OperationContract	 
] 
int 

CreateGame 
( 
String 
user "
)" #
;# $
[ 	
OperationContract	 
] 
int 
JoinGame 
( 
String 
user  
)  !
;! "
[ 	
OperationContract	 
] 
int 
	LeaveGame 
( 
String 
user !
,! "
int# &
game' +
)+ ,
;, -
} 
} Ú"
†C:\Users\Vik-t\Documents\Software Engineering\5to Semestre\Tecnolog√≠as para la Construcci√≥n\Proyecto\LISMAN\LismanService\LismanService\IMultiplayerManager.cs
	namespace 	
LismanService
 
{ 
[ 
ServiceContract 
( 
CallbackContract %
=& '
typeof( .
(. /'
IMultiplayerManagerCallBack/ J
)J K
)K L
]L M
public 

	interface 
IMultiplayerManager (
{) *
[		 	
OperationContract			 
(		 
IsOneWay		 #
=		$ %
true		& *
)		* +
]		+ ,
void

 
JoinMultiplayerGame

  
(

  !
String

! '
user

( ,
,

, -
int

. 1
idgame

2 8
)

8 9
;

9 :
[ 	
OperationContract	 
( 
IsOneWay #
=$ %
true& *
)* +
]+ ,
void 

MoveLisman 
( 
int 
idGame "
," #
String$ *
user+ /
,/ 0
int1 4
initialPositionX5 E
,E F
intG J
initialPositionYK [
,[ \
int] `
finalPositionXa o
,o p
intq t
finalPositionY	u É
)
É Ñ
;
Ñ Ö
} 
[ 
ServiceContract 
] 
public 

	interface '
IMultiplayerManagerCallBack 1
{2 3
[ 	
OperationContract	 
( 
IsOneWay #
=$ %
true& *
)* +
]+ ,
void #
PrintInformationPlayers $
($ %

Dictionary% /
</ 0
String0 6
,6 7
InformationPlayer7 H
>H I
listPlayersJ U
)U V
;V W
[ 	
OperationContract	 
( 
IsOneWay #
=$ %
true& *
)* +
]+ ,
void 
NotifyColorPlayer 
( 
int "
colorPlayer# .
,. /
String0 6
user7 ;
); <
;< =
[ 	
OperationContract	 
( 
IsOneWay #
=$ %
true& *
)* +
]+ ,
void 
NotifyLismanMoved 
( 
int "
colorPlayer# .
,. /
int0 3
	positionX4 =
,= >
int? B
	positionYC L
)L M
;M N
} 
[ 
DataContract 
] 
public 

class 
Game 
{ 
[ 	

DataMember	 
] 
public 
int 
[ 
, 
] 
gameMap 
{  
get! $
;$ %
set& )
;) *
}+ ,
[ 	

DataMember	 
] 
public 

Dictionary 
< 
String  
,  !
InformationPlayer" 3
>3 4
lismanUsers5 @
{A B
getC F
;F G
setH K
;K L
}M N
}!! 
[## 
DataContract## 
]## 
public$$ 

class$$ 
InformationPlayer$$ "
{%% 
[&& 	

DataMember&&	 
]&& 
public'' 
int'' 
colorLisman'' 
{''  
get''! $
;''$ %
set''& )
;'') *
}''+ ,
[(( 	

DataMember((	 
](( 
public)) 
int)) 
lifesLisman)) 
{))  
get))! $
;))$ %
set))& )
;))) *
}))+ ,
[** 	

DataMember**	 
]** 
public++ 
int++ 
scoreLisman++ 
{++  
get++! $
;++$ %
set++& )
;++) *
}+++ ,
[,, 	

DataMember,,	 
],, 
public-- 
bool-- 
hasPower-- 
{-- 
get-- "
;--" #
set--$ '
;--' (
}--) *
}.. 
}00 ÷7
öC:\Users\Vik-t\Documents\Software Engineering\5to Semestre\Tecnolog√≠as para la Construcci√≥n\Proyecto\LISMAN\LismanService\LismanService\LismanService.cs
	namespace

 	
LismanService


 
{

 
[ 
ServiceBehavior 
( 
InstanceContextMode (
=) *
InstanceContextMode+ >
.> ?

PerSession? I
)I J
]J K
public 

partial 
class 
LismanService &
:' (
IAccountManager) 8
{9 :
public 
int 

AddAccount 
( 
Account %
account& -
)- .
{ 	
try 
{ 
using 
( 
var 
dataBase #
=$ %
new& ) 
EntityModelContainer* >
(> ?
)? @
)@ A
{B C
var 

newAccount "
=# $
new% (

DataAccess) 3
.3 4
Account4 ;
{ 
User 
= 
account &
.& '
User' +
,+ ,
Password  
=! "
account# *
.* +
Password+ 3
,3 4
Registration_date )
=* +
account, 3
.3 4
Registration_date4 E
,E F
Key_confirmation (
=) *
account+ 2
.2 3
Key_confirmation3 C
,C D
Player 
=  
new! $

DataAccess% /
./ 0
Player0 6
{ 

First_name &
=' (
account) 0
.0 1
Player1 7
.7 8

First_name8 B
,B C
	Last_name %
=& '
account( /
./ 0
Player0 6
.6 7
	Last_name7 @
,@ A
Email !
=" #
account$ +
.+ ,
Player, 2
.2 3
Email3 8
} 
, 
Record 
=  
new! $

DataAccess% /
./ 0
Record0 6
{ 
Mult_best_score   +
=  , -
$num  . /
,  / 0
Mult_games_played!! -
=!!. /
$num!!0 1
,!!1 2
Mult_games_won"" *
=""+ ,
$num""- .
,"". /
Story_best_score## ,
=##- .
$num##/ 0
}$$ 
}%% 
;%% 
try&& 
{&& 
dataBase''  
.''  !

AccountSet''! +
.''+ ,
Add'', /
(''/ 0

newAccount''0 :
)'': ;
;''; <
return(( 
dataBase(( '
.((' (
SaveChanges((( 3
(((3 4
)((4 5
;((5 6
}** 
catch** 
(** '
DbEntityValidationException** 6
ex**7 9
)**9 :
{**; <
Logger++ 
.++ 
log++ "
.++" #
Error++# (
(++( )
ex++) +
)+++ ,
;++, -
return,, 
-,,  
$num,,  !
;,,! "
}-- 
}.. 
}// 
catch// 
(// 
	Exception// 
ex// 
)//  
{//! "
Logger00 
.00 
log00 
.00 
Error00  
(00  !
ex00! #
)00# $
;00$ %
return11 
-11 
$num11 
;11 
}22 
}33 	
public55 
Account55 
GetAccountById55 %
(55% &
int55& )
id55* ,
)55, -
{66 	
throw77 
new77 #
NotImplementedException77 -
(77- .
)77. /
;77/ 0
}88 	
public:: 
List:: 
<:: 
Account:: 
>:: 
GetAccounts:: (
(::( )
)::) *
{;; 	
throw<< 
new<< #
NotImplementedException<< -
(<<- .
)<<. /
;<</ 0
}== 	
public?? 
List?? 
<?? 
Record?? 
>?? 

GetRecords?? &
(??& '
)??' (
{@@ 	
tryAA 
{AA 
usingBB 
(BB 
varBB 
dataBaseBB #
=BB$ %
newBB& ) 
EntityModelContainerBB* >
(BB> ?
)BB? @
)BB@ A
{BBB C
returnCC 
dataBaseCC #
.CC# $
	RecordSetCC$ -
.CC- .
SelectCC. 4
(CC4 5
uCC5 6
=>CC7 9
newCC: =
RecordCC> D
{DD 
IdFF 
=FF 
uFF 
.FF 
IdFF !
,FF! "
Mult_best_scoreGG '
=GG( )
uGG* +
.GG+ ,
Mult_best_scoreGG, ;
,GG; <
Mult_games_playedHH )
=HH* +
uHH, -
.HH- .
Mult_games_playedHH. ?
,HH? @
Mult_games_wonII &
=II' (
uII) *
.II* +
Mult_games_wonII+ 9
,II9 :
Story_best_scoreJJ (
=JJ) *
uJJ+ ,
.JJ, -
Story_best_scoreJJ- =
,JJ= >
AccountKK 
=KK  !
newKK" %
AccountKK& -
{LL 
IdMM 
=MM  
uMM! "
.MM" #
IdMM# %
,MM% &
UserNN  
=NN! "
uNN# $
.NN$ %
AccountNN% ,
.NN, -
UserNN- 1
,NN1 2
PasswordOO $
=OO% &
uOO' (
.OO( )
AccountOO) 0
.OO0 1
PasswordOO1 9
,OO9 :
Registration_datePP -
=PP. /
uPP0 1
.PP1 2
AccountPP2 9
.PP9 :
Registration_datePP: K
,PPK L
Key_confirmationQQ ,
=QQ- .
uQQ/ 0
.QQ0 1
AccountQQ1 8
.QQ8 9
Key_confirmationQQ9 I
}RR 
}TT 
)TT 
.TT 
OrderByDescendingTT (
(TT( )
xTT) *
=>TT+ -
xTT. /
.TT/ 0
Story_best_scoreTT0 @
)TT@ A
.TTA B
ToListTTB H
(TTH I
)TTI J
;TTJ K
}UU 
}VV 
catchVV 
(VV 
	ExceptionVV 
exVV 
)VV  
{VV! "
LoggerWW 
.WW 
logWW 
.WW 
ErrorWW  
(WW  !
exWW! #
)WW# $
;WW$ %
returnXX 
nullXX 
;XX 
}YY 
}ZZ 	
public\\ 
void\\ 
test\\ 
(\\ 
string\\ 
message\\  '
)\\' (
{]] 	
throw^^ 
new^^ #
NotImplementedException^^ -
(^^- .
)^^. /
;^^/ 0
}__ 	
}`` 
}aa ’3
úC:\Users\Vik-t\Documents\Software Engineering\5to Semestre\Tecnolog√≠as para la Construcci√≥n\Proyecto\LISMAN\LismanService\LismanService\IAccountManager.cs
	namespace 	
LismanService
 
{ 
[ 
ServiceContract 
] 
public 

	interface 
IAccountManager $
{% &
[ 	
OperationContract	 
] 
int 

AddAccount 
( 
Account 
account &
)& '
;' (
[ 	
OperationContract	 
] 
List 
< 
Account 
> 
GetAccounts !
(! "
)" #
;# $
[ 	
OperationContract	 
] 
Account 
GetAccountById 
( 
int "
id# %
)% &
;& '
[ 	
OperationContract	 
] 
List 
< 
Record 
> 

GetRecords 
(  
)  !
;! "
} 
[ 
ServiceContract 
] 
public 

	interface 
ILoginManager "
{# $
[ 	
OperationContract	 
] 
Account 
LoginAccount 
( 
String #
user$ (
,( )
String* 0
password1 9
)9 :
;: ;
[ 	
OperationContract	 
] 
int 
LogoutAccount 
( 
String  
user! %
)% &
;& '
[   	
OperationContract  	 
]   
bool!! 
UserNameExists!! 
(!! 
String!! "
username!!# +
)!!+ ,
;!!, -
["" 	
OperationContract""	 
]"" 
bool## 
EmailExists## 
(## 
String## 
emailAdress##  +
)##+ ,
;##, -
}$$ 
['' 
DataContract'' 
]'' 
public(( 

partial(( 
class(( 
Account((  
{((! "
[)) 	

DataMember))	 
])) 
public** 
int** 
Id** 
{** 
get** 
;** 
set**  
;**  !
}**" #
[++ 	

DataMember++	 
]++ 
public,, 
string,, 
User,, 
{,, 
get,,  
;,,  !
set,," %
;,,% &
},,' (
[-- 	

DataMember--	 
]-- 
public.. 
string.. 
Password.. 
{..  
get..! $
;..$ %
set..& )
;..) *
}..+ ,
[// 	

DataMember//	 
]// 
public00 
string00 
Key_confirmation00 &
{00' (
get00) ,
;00, -
set00. 1
;001 2
}003 4
[11 	

DataMember11	 
]11 
public22 
string22 
Registration_date22 '
{22( )
get22* -
;22- .
set22/ 2
;222 3
}224 5
[33 	

DataMember33	 
]33 
public44 
virtual44 
Player44 
Player44 $
{44% &
get44' *
;44* +
set44, /
;44/ 0
}441 2
[55 	

DataMember55	 
]55 
public66 
virtual66 
Record66 
Record66 $
{66% &
get66' *
;66* +
set66, /
;66/ 0
}661 2
}77 
public88 

partial88 
class88 
Player88 
{88  !
[99 	

DataMember99	 
]99 
public:: 
int:: 
Id:: 
{:: 
get:: 
;:: 
set::  
;::  !
}::" #
[;; 	

DataMember;;	 
];; 
public<< 
string<< 

First_name<<  
{<<! "
get<<# &
;<<& '
set<<( +
;<<+ ,
}<<- .
[== 	

DataMember==	 
]== 
public>> 
string>> 
	Last_name>> 
{>>  !
get>>" %
;>>% &
set>>' *
;>>* +
}>>, -
[?? 	

DataMember??	 
]?? 
public@@ 
string@@ 
Email@@ 
{@@ 
get@@ !
;@@! "
set@@# &
;@@& '
}@@( )
[AA 	

DataMemberAA	 
]AA 
publicBB 
virtualBB 
AccountBB 
AccountBB &
{BB' (
getBB) ,
;BB, -
setBB. 1
;BB1 2
}BB3 4
}CC 
publicEE 

partialEE 
classEE 
RecordEE 
{EE  !
[FF 	

DataMemberFF	 
]FF 
publicGG 
intGG 
IdGG 
{GG 
getGG 
;GG 
setGG  
;GG  !
}GG" #
[HH 	

DataMemberHH	 
]HH 
publicII 
NullableII 
<II 
intII 
>II 
Mult_best_scoreII ,
{II- .
getII/ 2
;II2 3
setII4 7
;II7 8
}II9 :
[JJ 	

DataMemberJJ	 
]JJ 
publicKK 
NullableKK 
<KK 
intKK 
>KK 
Story_best_scoreKK -
{KK. /
getKK0 3
;KK3 4
setKK5 8
;KK8 9
}KK: ;
[LL 	

DataMemberLL	 
]LL 
publicMM 
NullableMM 
<MM 
intMM 
>MM 
Mult_games_playedMM .
{MM/ 0
getMM1 4
;MM4 5
setMM6 9
;MM9 :
}MM; <
[NN 	

DataMemberNN	 
]NN 
publicOO 
NullableOO 
<OO 
intOO 
>OO 
Mult_games_wonOO +
{OO, -
getOO. 1
;OO1 2
setOO3 6
;OO6 7
}OO8 9
[PP 	

DataMemberPP	 
]PP 
publicQQ 
virtualQQ 
AccountQQ 
AccountQQ &
{QQ' (
getQQ) ,
;QQ, -
setQQ. 1
;QQ1 2
}QQ3 4
}RR 
}SS ¶
ìC:\Users\Vik-t\Documents\Software Engineering\5to Semestre\Tecnolog√≠as para la Construcci√≥n\Proyecto\LISMAN\LismanService\LismanService\Logger.cs
	namespace 	
LismanService
 
{ 
class 	
Logger
 
{ 
public		 
static		 
readonly		 
log4net		 &
.		& '
ILog		' +
log		, /
=		0 1
log4net		2 9
.		9 :

LogManager		: D
.		D E
	GetLogger		E N
(		N O
System		O U
.		U V

Reflection		V `
.		` a

MethodBase		a k
.		k l
GetCurrentMethod		l |
(		| }
)		} ~
.		~ 
DeclaringType			 å
)
		å ç
;
		ç é
}

 
} æD
ôC:\Users\Vik-t\Documents\Software Engineering\5to Semestre\Tecnolog√≠as para la Construcci√≥n\Proyecto\LISMAN\LismanService\LismanService\LoginManager.cs
	namespace 	
LismanService
 
{ 
public 

partial 
class 
LismanService &
:' (
ILoginManager) 6
{7 8
public 
bool 
EmailExists 
(  
string  &
emailAdress' 2
)2 3
{ 	
try 
{ 
using 
( 
var 
dataBase #
=$ %
new& ) 
EntityModelContainer* >
(> ?
)? @
)@ A
{B C
int 
exists 
=  
dataBase! )
.) *
	PlayerSet* 3
.3 4
Where4 9
(9 :
u: ;
=>< >
u? @
.@ A
EmailA F
==G I
emailAdressJ U
)U V
.V W
CountW \
(\ ]
)] ^
;^ _
if 
( 
exists 
>  
$num! "
)" #
{$ %
return 
true #
;# $
} 
} 
} 
catch 
( 
	Exception 
ex !
)! "
{# $
Logger 
. 
log 
. 
Error  
(  !
ex! #
)# $
;$ %
} 
return 
false 
; 
} 	
public 
Account 
LoginAccount #
(# $
string$ *
user+ /
,/ 0
string1 7
password8 @
)@ A
{ 	
try 
{ 
using   
(   
var   
dataBase   #
=  $ %
new  & ) 
EntityModelContainer  * >
(  > ?
)  ? @
)  @ A
{  B C
int!! 
exists!! 
=!!  
dataBase!!! )
.!!) *

AccountSet!!* 4
.!!4 5
Where!!5 :
(!!: ;
u!!; <
=>!!= ?
u!!@ A
.!!A B
User!!B F
==!!G I
user!!J N
&!!O P
u!!Q R
.!!R S
Password!!S [
==!!\ ^
password!!_ g
)!!g h
.!!h i
Count!!i n
(!!n o
)!!o p
;!!p q
if"" 
("" 
exists"" 
>""  
$num""! "
)""" #
{""$ %
var## 

newAccount## &
=##' (
dataBase##) 1
.##1 2

AccountSet##2 <
.##< =
Where##= B
(##B C
u##C D
=>##E G
u##H I
.##I J
User##J N
==##O Q
user##R V
&##W X
u##Y Z
.##Z [
Password##[ c
==##d f
password##g o
)##o p
.##p q
Select##q w
(##w x
u##x y
=>##z |
new	##} Ä
Account
##Å à
{$$ 
Id%% 
=%%  
u%%! "
.%%" #
Id%%# %
,%%% &
User&&  
=&&! "
u&&# $
.&&$ %
User&&% )
,&&) *
Password'' $
=''% &
u''' (
.''( )
Password'') 1
,''1 2
Registration_date(( -
=((. /
u((0 1
.((1 2
Registration_date((2 C
,((C D
Key_confirmation)) ,
=))- .
u))/ 0
.))0 1
Key_confirmation))1 A
}** 
)** 
.** 
FirstOrDefault** )
(**) *
)*** +
;**+ ,
if++ 
(++ 

newAccount++ %
!=++& (
null++) -
)++- .
{++/ 0
if,, 
(,,  
!,,  !!
connectionChatService,,! 6
.,,6 7
ContainsKey,,7 B
(,,B C
user,,C G
),,G H
),,H I
{,,J K!
connectionChatService--  5
.--5 6
Add--6 9
(--9 :
user--: >
,--> ?
null--@ D
)--D E
;--E F
}.. 
Console// #
.//# $
	WriteLine//$ -
(//- .
$str//. K
,//K L

newAccount//M W
.//W X
User//X \
,//\ ]
DateTime//^ f
.//f g
Now//g j
)//j k
;//k l
}11 
return22 

newAccount22 )
;22) *
}33 
else33 
{33 
return44 
null44 #
;44# $
}55 
}66 
}77 
catch77 
(77 
	Exception77 
ex77 !
)77! "
{77# $
Logger88 
.88 
log88 
.88 
Error88  
(88  !
ex88! #
.88# $
Message88$ +
)88+ ,
;88, -
return99 
null99 
;99 
}:: 
};; 	
public== 
int== 
LogoutAccount==  
(==  !
string==! '
user==( ,
)==, -
{>> 	
if?? 
(?? !
connectionChatService?? %
.??% &
ContainsKey??& 1
(??1 2
user??2 6
)??6 7
)??7 8
{??9 :!
connectionChatService@@ %
.@@% &
Remove@@& ,
(@@, -
user@@- 1
)@@1 2
;@@2 3
}AA 
returnBB 
$numBB 
;BB 
}CC 	
publicEE 
boolEE 
UserNameExistsEE "
(EE" #
stringEE# )
usernameEE* 2
)EE2 3
{FF 	
tryGG 
{GG 
usingHH 
(HH 
varHH 
dataBaseHH #
=HH$ %
newHH& ) 
EntityModelContainerHH* >
(HH> ?
)HH? @
)HH@ A
{HHB C
intII 
existsII 
=II  
dataBaseII! )
.II) *

AccountSetII* 4
.II4 5
WhereII5 :
(II: ;
uII; <
=>II= ?
uII@ A
.IIA B
UserIIB F
==IIG I
usernameIIJ R
)IIR S
.IIS T
CountIIT Y
(IIY Z
)IIZ [
;II[ \
ifJJ 
(JJ 
existsJJ 
>JJ  
$numJJ! "
)JJ" #
{JJ$ %
returnKK 
trueKK #
;KK# $
}LL 
}MM 
}NN 
catchNN 
(NN 
	ExceptionNN 
exNN !
)NN! "
{NN# $
LoggerOO 
.OO 
logOO 
.OO 
ErrorOO  
(OO  !
exOO! #
)OO# $
;OO$ %
}PP 
returnQQ 
falseQQ 
;QQ 
}RR 	
publicUU 
AccountUU 
GetAccountByUserUU '
(UU' (
stringUU( .
userUU/ 3
)UU3 4
{VV 	
tryWW 
{WW 
usingXX 
(XX 
varXX 
dataBaseXX #
=XX$ %
newXX& ) 
EntityModelContainerXX* >
(XX> ?
)XX? @
)XX@ A
{XXB C
returnYY 
dataBaseYY #
.YY# $

AccountSetYY$ .
.YY. /
WhereYY/ 4
(YY4 5
uYY5 6
=>YY7 9
uYY: ;
.YY; <
UserYY< @
==YYA C
userYYD H
)YYH I
.YYI J
SelectYYJ P
(YYP Q
uYYQ R
=>YYS U
newYYV Y
AccountYYZ a
{ZZ 
Id[[ 
=[[ 
u[[ 
.[[ 
Id[[ !
,[[! "
User\\ 
=\\ 
u\\  
.\\  !
User\\! %
,\\% &
Password]]  
=]]! "
u]]# $
.]]$ %
Password]]% -
,]]- .
Registration_date^^ )
=^^* +
u^^, -
.^^- .
Registration_date^^. ?
,^^? @
}`` 
)`` 
.`` 
FirstOrDefault`` %
(``% &
)``& '
;``' (
}aa 
}bb 
catchbb 
(bb 
	Exceptionbb 
exbb 
)bb  
{bb! "
Loggercc 
.cc 
logcc 
.cc 
Errorcc  
(cc  !
excc! #
)cc# $
;cc$ %
returndd 
nulldd 
;dd 
}ee 
}ff 	
}gg 
}hh ‡V
üC:\Users\Vik-t\Documents\Software Engineering\5to Semestre\Tecnolog√≠as para la Construcci√≥n\Proyecto\LISMAN\LismanService\LismanService\MultiplayerManager.cs
	namespace		 	
LismanService		
 
{

 
public 

partial 
class 
LismanService &
:' (
IMultiplayerManager) <
{ 
static 
int 
[ 
, 
] 
GAMEMAP 
= 
new  #
int$ '
[' (
$num( *
,* +
$num, .
]. /
;/ 0
const 
int 
EMPTYBOX 
= 
$num 
; 
const 
int 
	POWERPILL 
= 
$num 
;  
const 
int 
LISMANYELLOW 
=  
$num! "
;" #
const 
int 

LISMANBLUE 
= 
$num  
;  !
const 
int 
	LISMANRED 
= 
$num 
;  
const 
int 
LISMANGREEN 
= 
$num  !
;! "
const 
int 
GHOST 
= 
$num 
; 
String 
parentDirectory 
=  
	Directory! *
.* +
	GetParent+ 4
(4 5
	Directory5 >
.> ?
GetCurrentDirectory? R
(R S
)S T
)T U
.U V
ParentV \
.\ ]
FullName] e
;e f
static 

Dictionary 
< 
String  
,  !'
IMultiplayerManagerCallBack" =
>= >!
connectionGameService? T
=U V
newW Z

Dictionary[ e
<e f
Stringf l
,l m(
IMultiplayerManagerCallBack	n â
>
â ä
(
ä ã
)
ã å
;
å ç
static 

Dictionary 
< 
int 
, 
Game #
># $&
multiplayerGameInformation% ?
=@ A
newB E

DictionaryF P
<P Q
intQ T
,T U
GameV Z
>Z [
([ \
)\ ]
;] ^
public 
void 
JoinMultiplayerGame '
(' (
string( .
user/ 3
,3 4
int5 8
idgame9 ?
)? @
{ 	
var 

connection 
= 
OperationContext -
.- .
Current. 5
.5 6
GetCallbackChannel6 H
<H I'
IMultiplayerManagerCallBackI d
>d e
(e f
)f g
;g h
if 
( !
connectionGameService %
.% &
ContainsKey& 1
(1 2
user2 6
)6 7
)7 8
{   !
connectionGameService!! %
[!!% &
user!!& *
]!!* +
=!!, -

connection!!. 8
;!!8 9
}"" 
else## 
{$$ !
connectionGameService%% %
.%%% &
Add%%& )
(%%) *
user%%* .
,%%. /

connection%%0 :
)%%: ;
;%%; <
}&& !
connectionGameService'' !
[''! "
user''" &
]''& '
.''' (
NotifyColorPlayer''( 9
(''9 :&
multiplayerGameInformation'': T
[''T U
idgame''U [
]''[ \
.''\ ]
lismanUsers''] h
[''h i
user''i m
]''m n
.''n o
colorLisman''o z
,''z {
user	''| Ä
)
''Ä Å
;
''Å Ç!
connectionGameService(( !
[((! "
user((" &
]((& '
.((' (#
PrintInformationPlayers((( ?
(((? @&
multiplayerGameInformation((@ Z
[((Z [
idgame(([ a
]((a b
.((b c
lismanUsers((c n
)((n o
;((o p
}** 	
public-- 
void-- 

MoveLisman-- 
(-- 
int-- "
idgame--# )
,--) *
String--* 0
user--1 5
,--5 6
int--7 :
initialPositionX--; K
,--K L
int--M P
initialPositionY--Q a
,--a b
int--c f
finalPositionX--g u
,--u v
int--w z
finalPositionY	--{ â
)
--â ä
{.. 	
foreach// 
(// 
var// 
userGame// !
in//" $
listGamesOnline//% 4
[//4 5
idgame//5 ;
]//; <
)//< =
{00 
try11 
{22 !
connectionGameService33 )
[33) *
userGame33* 2
]332 3
.333 4
NotifyLismanMoved334 E
(33E F&
multiplayerGameInformation33F `
[33` a
idgame33a g
]33g h
.33h i
lismanUsers33i t
[33t u
user33u y
]33y z
.33z {
colorLisman	33{ Ü
,
33Ü á
finalPositionX
33à ñ
,
33ñ ó
finalPositionY
33ò ¶
)
33¶ ß
;
33ß ®
}44 
catch44 
(44 "
CommunicationException44 /
e440 1
)441 2
{55 
Console66 
.66 
	WriteLine66 %
(66% &
$str66& L
+66M N
userGame66O W
+66X Y
$str66Z e
+66f g
e66h i
.66i j
Message66j q
)66q r
;66r s
}77 
}99 
}:: 	
public<< 
int<< 
GetValueBox<< 
(<< 
int<< "
idGame<<# )
,<<) *
int<<* -
finalPositionX<<. <
,<<< =
int<<> A
finalPositionY<<B P
)<<P Q
{== 	
int>> 
result>> 
=>> 
->> 
$num>> 
;>> 
int?? 
valuePosition?? 
=?? &
multiplayerGameInformation??  :
[??: ;
idGame??; A
]??A B
.??B C
gameMap??C J
[??J K
finalPositionX??K Y
,??Y Z
finalPositionY??[ i
]??i j
;??j k
switch@@ 
(@@ 
valuePosition@@ !
)@@! "
{AA 
caseBB 
EMPTYBOXBB 
:BB 
resultCC 
=CC 
$numCC 
;CC 
breakDD 
;DD 
caseEE 
	POWERPILLEE 
:EE 
resultFF 
=FF 
$numFF 
;FF 
breakGG 
;GG 
caseHH 
LISMANYELLOWHH !
:HH! "
resultII 
=II 
$numII 
;II 
breakJJ 
;JJ 
caseKK 
	LISMANREDKK 
:KK 
resultLL 
=LL 
$numLL 
;LL 
breakMM 
;MM 
caseNN 

LISMANBLUENN 
:NN  
resultOO 
=OO 
$numOO 
;OO 
breakPP 
;PP 
caseQQ 
LISMANGREENQQ  
:QQ  !
resultRR 
=RR 
$numRR 
;RR 
breakSS 
;SS 
caseTT 
GHOSTTT 
:TT 
resultUU 
=UU 
$numUU 
;UU 
breakVV 
;VV 
}WW 
returnYY 
resultYY 
;YY 
}ZZ 	
public\\ 
void\\ 
ReadMapGame\\ 
(\\  
)\\  !
{]] 	
String^^ 
ruta^^ 
=^^ 
$str	^^ µ
;
^^µ ∂
using__ 
(__ 
StreamReader__ 
sr__  "
=__# $
new__% (
StreamReader__) 5
(__5 6
parentDirectory__6 E
+__F G
$str__H \
)__\ ]
)__] ^
{`` 
forbb 
(bb 
intbb 
ibb 
=bb 
$numbb 
;bb 
ibb  !
<=bb" $
$numbb% '
;bb' (
ibb) *
++bb* ,
)bb, -
{cc 
fordd 
(dd 
intdd 
jdd 
=dd  
$numdd! "
;dd" #
jdd$ %
<=dd& (
$numdd) +
;dd+ ,
jdd- .
++dd. 0
)dd0 1
{ee 
intff 
caracterff $
=ff% &
srff' )
.ff) *
Readff* .
(ff. /
)ff/ 0
;ff0 1
ifhh 
(hh 
caracterhh $
!=hh% '
-hh( )
$numhh) *
)hh* +
{ii 
switchjj "
(jj# $
caracterjj$ ,
)jj, -
{kk 
casell  $
$numll% '
:ll' (
GAMEMAPmm$ +
[mm+ ,
imm, -
,mm- .
jmm/ 0
]mm0 1
=mm2 3
$nummm4 5
;mm5 6
breaknn$ )
;nn) *
caseoo  $
$numoo% '
:oo' (
GAMEMAPpp$ +
[pp+ ,
ipp, -
,pp- .
jpp/ 0
]pp0 1
=pp2 3
$numpp4 5
;pp5 6
breakqq$ )
;qq) *
caserr  $
$numrr% '
:rr' (
GAMEMAPss$ +
[ss+ ,
iss, -
,ss- .
jss/ 0
]ss0 1
=ss2 3
$numss4 5
;ss5 6
breaktt$ )
;tt) *
caseuu  $
$numuu% '
:uu' (
GAMEMAPvv$ +
[vv+ ,
ivv, -
,vv- .
jvv/ 0
]vv0 1
=vv2 3
$numvv4 5
;vv5 6
breakww$ )
;ww) *
casexx  $
$numxx% '
:xx' (
GAMEMAPyy$ +
[yy+ ,
iyy, -
,yy- .
jyy/ 0
]yy0 1
=yy2 3
$numyy4 5
;yy5 6
breakzz$ )
;zz) *
case{{  $
$num{{% '
:{{' (
GAMEMAP||$ +
[||+ ,
i||, -
,||- .
j||/ 0
]||0 1
=||2 3
$num||4 5
;||5 6
break}}$ )
;}}) *
case~~  $
$num~~% '
:~~' (
GAMEMAP$ +
[+ ,
i, -
,- .
j/ 0
]0 1
=2 3
$num4 5
;5 6
break
ÄÄ$ )
;
ÄÄ) *
case
ÅÅ  $
$num
ÅÅ% '
:
ÅÅ' (
GAMEMAP
ÇÇ$ +
[
ÇÇ+ ,
i
ÇÇ, -
,
ÇÇ- .
j
ÇÇ/ 0
]
ÇÇ0 1
=
ÇÇ2 3
$num
ÇÇ4 5
;
ÇÇ5 6
break
ÉÉ$ )
;
ÉÉ) *
}
ÖÖ 
}
áá 
}
ââ 
}
ää 
}
åå 
}
éé 	
}
èè 
}êê Ã
§C:\Users\Vik-t\Documents\Software Engineering\5to Semestre\Tecnolog√≠as para la Construcci√≥n\Proyecto\LISMAN\LismanService\LismanService\Properties\AssemblyInfo.cs
[ 
assembly 	
:	 

AssemblyTitle 
( 
$str (
)( )
]) *
[		 
assembly		 	
:			 

AssemblyDescription		 
(		 
$str		 !
)		! "
]		" #
[

 
assembly

 	
:

	 
!
AssemblyConfiguration

  
(

  !
$str

! #
)

# $
]

$ %
[ 
assembly 	
:	 

AssemblyCompany 
( 
$str 
) 
] 
[ 
assembly 	
:	 

AssemblyProduct 
( 
$str *
)* +
]+ ,
[ 
assembly 	
:	 

AssemblyCopyright 
( 
$str 0
)0 1
]1 2
[ 
assembly 	
:	 

AssemblyTrademark 
( 
$str 
)  
]  !
[ 
assembly 	
:	 

AssemblyCulture 
( 
$str 
) 
] 
[ 
assembly 	
:	 


ComVisible 
( 
false 
) 
] 
[ 
assembly 	
:	 

Guid 
( 
$str 6
)6 7
]7 8
[## 
assembly## 	
:##	 

AssemblyVersion## 
(## 
$str## $
)##$ %
]##% &
[$$ 
assembly$$ 	
:$$	 

AssemblyFileVersion$$ 
($$ 
$str$$ (
)$$( )
]$$) *
[%% 
assembly%% 	
:%%	 

log4net%% 
.%% 
Config%% 
.%% 
XmlConfigurator%% )
(%%) *

ConfigFile%%* 4
=%%5 6
$str%%7 G
)%%G H
]%%H I