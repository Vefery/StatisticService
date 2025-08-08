import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { LeftPanelComponent } from './Components/left-panel/left-panel.component';
import { ListEntryComponent } from './Components/list-entry/list-entry.component';
import { DetailsPageComponent } from './Components/details-page/details-page.component';
import { DetailEntryComponent } from './Components/detail-entry/detail-entry.component';

@NgModule({
  declarations: [
    AppComponent,
    LeftPanelComponent,
    ListEntryComponent,
    DetailsPageComponent,
    DetailEntryComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
