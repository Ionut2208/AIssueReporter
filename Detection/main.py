import uuid

from fastapi import FastAPI, File, UploadFile
from fastapi.responses import JSONResponse
import uvicorn
import shutil
import os
import subprocess
from pathlib import Path
import sys
app = FastAPI()


@app.post("/detect/")
async def detect(file: UploadFile = File(...)):
    # Save uploaded image to disk
    temp_path = f"temp_images/{file.filename}"
    os.makedirs("temp_images", exist_ok=True)

    with open(temp_path, "wb") as buffer:
        shutil.copyfileobj(file.file, buffer)

    BASE_DIR = Path(__file__).resolve().parent  # Directory where main.py is

    detect_script = BASE_DIR / "yolov5" / "detect.py"
    weights_path = BASE_DIR / "yolov5" / "best.pt"
    result_path = BASE_DIR / "runs" / "detect"
    unique_id = str(uuid.uuid4())[:8]
    run_name = f"exp_{unique_id}"

    # In your route:
    command = [
        sys.executable,
        str(detect_script),
        "--weights", str(weights_path),
        "--img", "640",
        "--conf", "0.4",
        "--source", str(temp_path),
        "--save-txt",
        "--save-conf",
        "--project", result_path,
        "--name", run_name,
        "--exist-ok"
    ]


    process = subprocess.run(command, stdout=subprocess.PIPE, stderr=subprocess.PIPE)

    if process.returncode != 0:
        print("Subprocess failed:")
        print(process.stderr.decode())

        return JSONResponse({
            "status": "error",
            "message": process.stderr.decode()
        }, status_code=500)


    os.remove(temp_path)


    output_image_path = os.path.join(result_path, run_name, file.filename).replace("\\", "/")
    labels_path = os.path.join(result_path, run_name, "labels", f"{os.path.splitext(file.filename)[0]}.txt").replace(
        "\\", "/")



    return JSONResponse({
        "status": "success",
        "output_image": str(output_image_path),
        "details": str(labels_path)
    })


if __name__ == "__main__":
    uvicorn.run("main:app", host="127.0.0.1", port=8000)
