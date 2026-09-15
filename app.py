# app.py
# A minimal Flask web server for Emmanuel Kimutai's portfolio site.
# It serves index.html, index.css, and script.js as static files,
# plus a small /api/contact endpoint you could wire a contact form to later.
#
# Setup:
#   1. Install Flask:              pip install flask
#   2. Put this file in your project's root folder.
#   3. Create a "static" folder next to it, and put
#      index.html, index.css, and script.js inside "static".
#   4. Run it:                     python app.py
#   5. Open the URL shown in the terminal (e.g. http://localhost:5000).

from flask import Flask, request, jsonify, send_from_directory

app = Flask(__name__, static_folder="static", static_url_path="")


@app.route("/")
def home():
    # Serves index.html when someone visits the site's root URL.
    return send_from_directory(app.static_folder, "index.html")


@app.route("/api/contact", methods=["POST"])
def contact():
    data = request.get_json(silent=True) or {}
    name = (data.get("name") or "").strip()
    email = (data.get("email") or "").strip()
    text = (data.get("text") or "").strip()

    if not name or not email:
        return jsonify({"error": "Name and email are required."}), 400

    # For now this just logs the message to the console.
    # Later you could save it to a database or send an email instead.
    print(f"New contact message from {name} ({email}): {text}")

    return jsonify({"status": "received"})


if __name__ == "__main__":
    app.run(debug=True)