<template>
    <!-- WRAPPER -->
    <div>
        <h2 class="m-t-0"> {{ this.surplusValue }}</h2>
</div>

    <!-- END WRAPPER -->
</template>

<script>
    export default {
    props:['fundtypeid','statusid'],
        data() {
            return {
                surplusValue: 0,
            }
        },
    mounted() {
        axios
            .get(`/api/DashBoardWidget/getLoanAppCountByStatus/${this.fundtypeid}`)
            .then(response => (this.surplusValue = response.data.data))
      
     }
};
</script>