import express, { Express, Request, Response } from "express";
import cors, { CorsOptions } from "cors";
import { chromium } from "playwright";

const app: Express = express();
const port = 8080;
const corsOpts: CorsOptions = {
  origin: [ "http://localhost:3000" ],
  methods: [ "GET" ]
}

app.get("/", cors(corsOpts), async (req: Request, res: Response) => {
  const url = decodeURIComponent(req.query.url as string)

  const browser = await chromium.launch({
    headless: true
  });

  const context = await browser.newContext();
  const page = await context.newPage();
  await page.goto(url);
  await page.waitForLoadState("domcontentloaded");
  var text = await page.evaluate("document.body.innerText");

  res.status(200).send(text);
})

app.listen(port, () => {
  console.log(`[Server]: I am running at http://localhost:${port}`);
});