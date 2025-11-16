import http from "k6/http";
import { sleep } from "k6";

export const options = {
  stages: [
    // --- Spike phase ---
    { duration: "10s", target: 0 },     // start
    { duration: "5s", target: 500 },    // sudden spike to 500 VUs
    { duration: "20s", target: 500 },   // hold spike briefly

    // --- Further ramp-up ---
    { duration: "10s", target: 1000 },    // continue to ramp up
    { duration: "30s", target: 1000 },    // sustain high load

    // --- Ramp-down ---
    { duration: "20s", target: 0 },     // cool down
  ],
};

export default function () {
  const url = "https://localhost:7000/v1/pages/page?id=/<path>";  // 🔹 Replace with your endpoint

  const headers = {
    "x-site-host": "<domain>",
    "x-site-path": "/<path>"
  };

  http.get(url, { headers });

  sleep(1);
}
