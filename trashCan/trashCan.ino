int ENA = 9;
int IN1 = 8;
int IN2 = 7;
int receiveVal = 20;   
int x = 1;


void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  pinMode (IN1, OUTPUT);
  pinMode (IN2, OUTPUT);
  pinMode (ENA, OUTPUT);
}

void loop() {
  // put your main code here, to run repeatedly  
     
  if(Serial.available() > 0){       
  receiveVal = Serial.parseInt();
  }
   
  if (receiveVal == 0){ //Trash
//    analogWrite(ENA,255);
//    digitalWrite(IN1, LOW);
//    digitalWrite(IN2, HIGH);
//    delay(3000);
//    analogWrite(ENA,0);
//    receiveVal = 20;    
  }
  else if (receiveVal == 1){ //Recycle
    analogWrite(ENA,255);
    digitalWrite(IN1, HIGH);
    digitalWrite(IN2, LOW);
    delay(6000);
    digitalWrite(IN1, LOW);
    digitalWrite(IN2, HIGH);
    delay(6000);
    analogWrite(ENA,0);
    receiveVal = 20;   
  }
      
}
