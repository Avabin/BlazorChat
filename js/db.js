import { getDatabase, ref, set } from "firebase/database";

const database = getDatabase();

function writeMessageData(message) {
    const db = getDatabase();
    set(ref(db, 'messages/' + userId), {
        username: name,
        email: email,
        profile_picture : imageUrl
    });
}