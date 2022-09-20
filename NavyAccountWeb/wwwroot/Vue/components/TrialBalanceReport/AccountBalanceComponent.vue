<template>
    <!-- WRAPPER -->
    <div>

        <div v-if="errors" class="alert alert-danger alert-dismissible" role="alert">
            <div class="alert-message">
                {{ [errors] }}
            </div>
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">�</span>
            </button>
        </div>
        <div v-if="responseMessage" class="alert alert-primary alert-dismissible" role="alert"> <div class="alert-message"> {{ responseMessage }} </div> </div>
        <div class="card-body">

            <div class="row">
                 <div class="col-12 col-xl-6">
                    <div class="form-group">
                        <label class="form-label">Indicator</label>

                        <select class="form-control" v-model="postBody.indicator" name="indicator" required>
                            <option v-for="loantype in IndicatorList" v-bind:value="loantype" v-bind:key="loantype"> {{ loantype }} </option>
                        </select>

                    </div>
                </div>
                <div class="col-12 col-xl-6">
                    <div class="form-group">
                        <label class="form-label">Year</label>
                        <input class="form-control" name="description" v-model="postBody.year" />
                    </div>
                </div>
                 <div class="col-12 col-xl-6" v-show="postBody.indicator=='Specific Main ledger'">
                    <div class="form-group">
                        <label class="form-label">Main Account</label>
                        <select class="form-control" v-model="postBody.mainacct" name="mainacct" required>
                            <option v-for="loantype in mainacctList" v-bind:value="loantype.code" v-bind:key="loantype.code"> {{ loantype.desc}} </option>
                        </select>
                    </div>
                </div>

                  <div class="col-12 ">
                        <div class="btn-group mr-2 sw-btn-group-extra" role="group">
                            <button class="btn btn-submit btn-primary" v-on:click="checkForm" type="submit">{{submitorUpdate}}</button>
                        </div>
                 </div>
           
            </div>

        </div>

    </div>
 
    <!-- END WRAPPER -->
</template>

<script>
    import vuejsAutocomplete from 'vuejs-auto-complete'
export default {
    props:['fundtypeid'],
    data() {
        return {
        responseMessage:'',
            errors: null,
            searchData: '',
            submitorUpdate: 'submit',
            mainacctList: null,
            autoselectenabled: false,
            canProcess: true,
            IndicatorList:["Main Ledger","Subsidiary Ledger","Specific Main ledger"],
            wantshow:false,
            wantshow2:true,
            wantshow3:false,
            claim:null,
        postBody: {
            indicator:'',
            year:'',
            mainacct:'opo'
  
        },
        };
        },
        mounted() {
            this.$store.state.objectToUpdate = null,
        axios
            .get('/TrialbalanceReport/GetMainAct')
            .then(response => (this.mainacctList = response.data))
       
     },
     methods: {
         processReport:function(){
            //saxios.post(`/TrialbalanceReport/ProcessReport`,this.postBody);
            window.open(`/TrialbalanceReport/ProcessReport/${this.postBody.indicator}/${this.postBody.year}/${this.postBody.mainacct}`, "_blank"); 
         },
         processRetrieve : function (discharges) {
            this.$store.state.objectToUpdate = discharges;
            this.wantshow=true;
             this.wantshow2=false;
         },
         setValuePersonID(result) {
             axios.get(`/api/Claimtype/GetClaim/${result.value}/${this.postBody.FundTypeID}`)
                        .then(response => {this.claimlist=response.data})                        
           },
         checkForm: function (e) {
             if (this.postBody.indicator) {
            //     alert(this.postBody.indicator);
              e.preventDefault();
              this.canProcess = false;
              this.processReport();
          }
          else{

          this.errors = [];
            this.errors.push('Required');
          }
        },
        postPost() {
            if (this.submitorUpdate == 'submit') {
                    axios.post(`/TrialbalanceReport/ProcessReport`, this.postBody )
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;
                            this.canProcess = true;
                            //  this.wantshow3=true;
                            if (response.data.responseCode == '200') {
                                this.postBody.PersonID = ''; this.postBody.appdate = '';
                                this.wanttoupdate = true;
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
            }


        } 
        }
     }

</script>