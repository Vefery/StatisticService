import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { LeftPanelComponent } from './Components/left-panel/left-panel.component';
import { ListEntryComponent } from './Components/list-entry/list-entry.component';

@NgModule({
  declarations: [
    AppComponent,
    LeftPanelComponent,
    ListEntryComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
