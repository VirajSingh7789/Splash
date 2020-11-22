import cv2
import time

cap = cv2.VideoCapture(0)
count = 0;
while(count < 1):
# while (True):
    test, frame = cap.read()
    cv2.imshow("Preview",frame)
    cv2.imwrite("frame%d.jpg" % count, frame) 
    time.sleep(4)
    count = count + 1
    if cv2.waitKey(1) & 0xFF == ord('q'):    
        break
    
cap.release()
cv2.destroyAllWindows()